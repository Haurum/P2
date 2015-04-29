using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace OrienteeringTracker
{
    static class Helper
    {
        public static Runner ReadGPXData(FileStream GpxStream)
        {
            Runner route = new Runner();
            GpxReader reader = new GpxReader(GpxStream);
            reader.Read();
            route.Date = reader.Track.Segments[0].TrackPoints[0].Time;
            route.RunnerName = Path.GetFileNameWithoutExtension(GpxStream.Name);


            foreach (GpxPoint gp in reader.Track.Segments[0].TrackPoints)
            {
                double UTMNorthing;
                double UTMEasting;
                string Zone;
                ConvertLatLongtoUTM(gp.Latitude, gp.Longitude, out UTMNorthing, out UTMEasting, out Zone);

                float x;
                float y;
                ConvertUTMToPixel(UTMNorthing, UTMEasting, out x, out y);

                Coordinate c = new Coordinate(x, y, gp.Time, (float)(UTMEasting), (float)(UTMNorthing));
                route.Coords.Add(c);
            }
            GpxStream.Close();
            AddMissingPoints(ref route);
            return route;
        }

        public static List<RunnerData> GetPosAndDiff(List<RunnerData> runnerData, List<Runner> runners)
        {
            int pos = 1;
            runnerData = runnerData.OrderBy(runner => runner.time).ToList();
            for (int i = 0; i < runnerData.Count; i++ )
            {
                if (runnerData[i].reached)
                {
                    runnerData[i].pos = pos;
                    if (runnerData[i].pos != 1)
                    {
                        runnerData[i].diff = runnerData[i-1].diff + runnerData[i].time - runnerData[i-1].time;
                    }
                    pos++;
                }
                else
                {
                    runnerData[i].pos = -1;
                    runnerData[i].diff = new TimeSpan(0);
                }
            }
            return runnerData;
        }

        public static void AddMissingPoints(ref Runner route)
        {
            int counter = 0;
            while (counter < route.Coords.Count-1)
            {
                if (route.Coords[counter].Time >= route.Coords[counter + 1].Time)
                {
                    route.Coords.RemoveAt(counter + 1);
                    counter--;
                }
                else if (route.Coords[counter].Time.AddSeconds(1) != route.Coords[counter + 1].Time)
                {
                    Coordinate temp = route.Coords[counter];
                    temp.Time = temp.Time.AddSeconds(1);
                    route.Coords.Insert(counter + 1, temp);
                }
                counter++;
            }
        }

        public static List<ControlPoint> ReadControlPoints(string path)
        {
            string[] coordinatesString;
            int i = 0;
            List<ControlPoint> controlPoints = new List<ControlPoint>();
            foreach (var line in File.ReadLines(path))
            {
                coordinatesString = line.Split(';');
                //float x;
                //float y;
                //ConvertUTMToPixel(Convert.ToDouble(coordinatesString[0]), Convert.ToDouble(coordinatesString[1]), out x, out y);

                //controlPoints.Add(new ControlPoint() { Cord = new Coordinate(float.Parse(coordinatesString[0], System.Globalization.CultureInfo.InvariantCulture), float.Parse(coordinatesString[1], System.Globalization.CultureInfo.InvariantCulture), DateTime.Now, 0,0), Radius = 10, Number = i });
                controlPoints.Add(new ControlPoint() { Cord = new Coordinate(float.Parse(coordinatesString[2], System.Globalization.CultureInfo.InvariantCulture), float.Parse(coordinatesString[3], System.Globalization.CultureInfo.InvariantCulture), DateTime.Now, float.Parse(coordinatesString[0], System.Globalization.CultureInfo.InvariantCulture),float.Parse(coordinatesString[1], System.Globalization.CultureInfo.InvariantCulture)), Radius = 10, Number = i });
                i++;
            }
            return controlPoints;
        }

        public static ControlPointTime ControlPointChecker(ControlPoint cp, Runner r)
        {
            List<ControlPointTime> distList = new List<ControlPointTime>();
            ControlPointTime cpt = new ControlPointTime();
            double doubleDist = 0;
            foreach (Coordinate coord in r.Coords)
            {
                doubleDist = CalcSingleLength(coord.pixelPoint.X, coord.pixelPoint.Y, cp.Cord.pixelPoint.X, cp.Cord.pixelPoint.Y);
                if (doubleDist < 25)
                {
                    for (int i = r.Coords.IndexOf(coord); i < r.Coords.Count; i++)
                    {
                        if (CalcSingleLength(r.Coords[i].pixelPoint.X, r.Coords[i].pixelPoint.Y, cp.Cord.pixelPoint.X, cp.Cord.pixelPoint.Y) > 25)
                        {
                            return distList.OrderBy(distance => distance.Dist).First();
                        }
                        cpt = new ControlPointTime();
                        cpt.Cord = cp.Cord;
                        cpt.Number = cp.Number;
                        cpt.Second = r.Coords.IndexOf(coord);
                        cpt.Dist = CalcSingleLength(r.Coords[i].pixelPoint.X, r.Coords[i].pixelPoint.Y, cp.Cord.pixelPoint.X, cp.Cord.pixelPoint.Y);
                        cpt.Second = i;
                        distList.Add(cpt);
                    }
                }
            }
            return new ControlPointTime();

        }

        public static double CalcTotalLength(Runner route, int startPoint, int endPoint)
        {
            double Res = 0;
            for (int i = startPoint; i < endPoint; i++)
            {
                Res += CalcSingleLength(route.Coords[i].UTMEast, route.Coords[i].UTMNorth, route.Coords[i + 1].UTMEast, route.Coords[i + 1].UTMNorth);
            }
            return Res;
        }
        
        public static double CalcSingleLength(double startPoint_X, double startPoint_Y, double endPoint_X, double endPoint_Y)
        {
            return Math.Sqrt(Math.Pow(startPoint_X - endPoint_X, 2) + Math.Pow(startPoint_Y - endPoint_Y, 2));
        }

        public static double CalcSpeedMinsPrKm(double length, int startTick, int endTick)
        {
            int deltaTick = endTick - startTick;
            return CalcSpeedMinsPrKm(length, deltaTick);
        }

        public static double CalcSpeedMinsPrKm(double length, int deltaTick)
        {
            double secPrMeter = deltaTick / length;
            double result = secPrMeter * 1000 / 60;
            return result;
        }

        public static void ConvertUTMToPixel(double UTM_north, double UTM_east, out float x, out float y)
        {
            MemoryStream ms = new MemoryStream(OrienteeringTracker.Properties.Resources.Hjermind_Egekrat_ref_ref1);
            string line;
            List<float> worldTal = new List<float>();
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);

            while ((line = sr.ReadLine()) != null)
            {
                worldTal.Add(float.Parse(line, CultureInfo.InvariantCulture));
            }

            x = Convert.ToInt32(((worldTal[3] * UTM_east) - (worldTal[3] * worldTal[4])) / (worldTal[0] * worldTal[3]));
            y = Convert.ToInt32(((worldTal[0] * UTM_north) - (worldTal[0] * worldTal[5])) / (worldTal[0] * worldTal[3]));
        }

        public static void ConvertLatLongtoUTM(double Lat, double Long, out double UTMNorthing, out double UTMEasting, out string Zone)
        {

            double a = 6378137; //WGS84
            double eccSquared = 0.00669438; //WGS84
            double k0 = 0.9996;

            double LongOrigin;
            double eccPrimeSquared;
            double N, T, C, A, M;

            //Make sure the longitude is between -180.00 .. 179.9
            double LongTemp = (Long + 180) - ((int)((Long + 180) / 360)) * 360 - 180; // -180.00 .. 179.9;
            double deg2rad = (Math.PI / 180);
            double LatRad = Lat * deg2rad;
            double LongRad = LongTemp * deg2rad;
            double LongOriginRad;
            int ZoneNumber;

            ZoneNumber = ((int)((LongTemp + 180) / 6)) + 1;

            if (Lat >= 56.0 && Lat < 64.0 && LongTemp >= 3.0 && LongTemp < 12.0)
                ZoneNumber = 32;

            // Special zones for Svalbard
            if (Lat >= 72.0 && Lat < 84.0)
            {
                if (LongTemp >= 0.0 && LongTemp < 9.0) ZoneNumber = 31;
                else if (LongTemp >= 9.0 && LongTemp < 21.0) ZoneNumber = 33;
                else if (LongTemp >= 21.0 && LongTemp < 33.0) ZoneNumber = 35;
                else if (LongTemp >= 33.0 && LongTemp < 42.0) ZoneNumber = 37;
            }
            LongOrigin = (ZoneNumber - 1) * 6 - 180 + 3; //+3 puts origin in middle of zone
            LongOriginRad = LongOrigin * deg2rad;

            //compute the UTM Zone from the latitude and longitude
            Zone = ZoneNumber.ToString() + UTMLetterDesignator(Lat);

            eccPrimeSquared = (eccSquared) / (1 - eccSquared);

            N = a / Math.Sqrt(1 - eccSquared * Math.Sin(LatRad) * Math.Sin(LatRad));
            T = Math.Tan(LatRad) * Math.Tan(LatRad);
            C = eccPrimeSquared * Math.Cos(LatRad) * Math.Cos(LatRad);
            A = Math.Cos(LatRad) * (LongRad - LongOriginRad);

            M = a * ((1 - eccSquared / 4 - 3 * eccSquared * eccSquared / 64 - 5 * eccSquared * eccSquared * eccSquared / 256) * LatRad
            - (3 * eccSquared / 8 + 3 * eccSquared * eccSquared / 32 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(2 * LatRad)
            + (15 * eccSquared * eccSquared / 256 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(4 * LatRad)
            - (35 * eccSquared * eccSquared * eccSquared / 3072) * Math.Sin(6 * LatRad));

            UTMEasting = (double)(k0 * N * (A + (1 - T + C) * A * A * A / 6
            + (5 - 18 * T + T * T + 72 * C - 58 * eccPrimeSquared) * A * A * A * A * A / 120)
            + 500000.0);

            UTMNorthing = (double)(k0 * (M + N * Math.Tan(LatRad) * (A * A / 2 + (5 - T + 9 * C + 4 * C * C) * A * A * A * A / 24
            + (61 - 58 * T + T * T + 600 * C - 330 * eccPrimeSquared) * A * A * A * A * A * A / 720)));
            if (Lat < 0)
                UTMNorthing += 10000000.0; //10000000 meter offset for southern hemisphere
        }


        private static char UTMLetterDesignator(double Lat)
        {
            char LetterDesignator;

            if ((84 >= Lat) && (Lat >= 72)) LetterDesignator = 'X';
            else if ((72 > Lat) && (Lat >= 64)) LetterDesignator = 'W';
            else if ((64 > Lat) && (Lat >= 56)) LetterDesignator = 'V';
            else if ((56 > Lat) && (Lat >= 48)) LetterDesignator = 'U';
            else if ((48 > Lat) && (Lat >= 40)) LetterDesignator = 'T';
            else if ((40 > Lat) && (Lat >= 32)) LetterDesignator = 'S';
            else if ((32 > Lat) && (Lat >= 24)) LetterDesignator = 'R';
            else if ((24 > Lat) && (Lat >= 16)) LetterDesignator = 'Q';
            else if ((16 > Lat) && (Lat >= 8)) LetterDesignator = 'P';
            else if ((8 > Lat) && (Lat >= 0)) LetterDesignator = 'N';
            else if ((0 > Lat) && (Lat >= -8)) LetterDesignator = 'M';
            else if ((-8 > Lat) && (Lat >= -16)) LetterDesignator = 'L';
            else if ((-16 > Lat) && (Lat >= -24)) LetterDesignator = 'K';
            else if ((-24 > Lat) && (Lat >= -32)) LetterDesignator = 'J';
            else if ((-32 > Lat) && (Lat >= -40)) LetterDesignator = 'H';
            else if ((-40 > Lat) && (Lat >= -48)) LetterDesignator = 'G';
            else if ((-48 > Lat) && (Lat >= -56)) LetterDesignator = 'F';
            else if ((-56 > Lat) && (Lat >= -64)) LetterDesignator = 'E';
            else if ((-64 > Lat) && (Lat >= -72)) LetterDesignator = 'D';
            else if ((-72 > Lat) && (Lat >= -80)) LetterDesignator = 'C';
            else LetterDesignator = 'Z'; //Latitude is outside the UTM limits
            return LetterDesignator;
        }

        class Ellipsoid
        {
            //Attributes
            public string ellipsoidName;
            public double EquatorialRadius;
            public double eccentricitySquared;

            public Ellipsoid(string name, double radius, double ecc)
            {
                ellipsoidName = name;
                EquatorialRadius = radius;
                eccentricitySquared = ecc;
            }
        };
    }
}
