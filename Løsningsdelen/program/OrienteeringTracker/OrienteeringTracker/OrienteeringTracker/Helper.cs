using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OrienteeringTracker
{
    static class Helper
    {
        public static Route ReadGPXData(Stream GpxStream)
        {
            Route route = new Route();
            GpxReader reader = new GpxReader(GpxStream);
            reader.Read();
            route.Date = reader.Track.Segments[0].TrackPoints[0].Time;

            foreach (GpxPoint gp in reader.Track.Segments[0].TrackPoints)
            {
                double UTMNorthing;
                double UTMEasting;
                string Zone;
                ConvertLatLongtoUTM(gp.Latitude, gp.Longitude, out UTMNorthing, out UTMEasting, out Zone);

                float x;
                float y;
                ConvertUTMToPixel(UTMNorthing, UTMEasting, out x, out y);

                Coordinate c = new Coordinate(x, y, gp.Time);
                route.Coords.Add(c);
            }
            return route;
        }

        public static void ConvertUTMToPixel(double UTM_north, double UTM_east, out float x, out float y)
        {
            StreamReader sr = new StreamReader(Encoding.UTF8.GetString(OrienteeringTracker.Properties.Resources.Hjermind_Egekrat_ref_ref1));
            string line;
            List<double> worldTal = new List<double>();

            while ((line = sr.ReadLine()) != null)
            {
                worldTal.Add(Convert.ToDouble(line));
            }

            /*x = Convert.ToInt32(((-0.84964441 * UTM_east) - (-0.84964441 * 539276.35483168)) / (0.84964441 * -0.84964441));
            y = Convert.ToInt32(((0.84964441 * UTM_north) - (0.84964441 * 6250863.73506770)) / (0.84964441 * -0.84964441));*/
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
