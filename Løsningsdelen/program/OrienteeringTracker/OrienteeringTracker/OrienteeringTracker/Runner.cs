using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrienteeringTracker
{
    public class Runner
    {
        public Runner()
        {
            Coords = new List<Coordinate>();
            Visited = new List<ControlPointTime>();
	        startingSecond = new List<int>();
        }

        public string RunnerName { get; set; }
        public DateTime Date { get; set; }
        public System.Drawing.Color RouteColor { get; set; }
        public bool reachedAll { get; set; }
        public List<int> startingSecond { get; set; }


        public List<Coordinate> Coords { get; set; }


        public List<ControlPointTime> Visited { get; set; }

		// Bruger input GPX data til at indsætte data i instansen af Runner.
        public void ReadGPXData(FileStream GpxStream)
        {
            GpxReader reader = new GpxReader(GpxStream);
            reader.Read();
            this.Date = reader.Track.Segments[0].TrackPoints[0].Time;
            this.RunnerName = Path.GetFileNameWithoutExtension(GpxStream.Name);

			// Kører gennem hvert koordinat i GPX-filen
            foreach (GpxPoint gp in reader.Track.Segments[0].TrackPoints)
            {
                double UTMNorthing;
                double UTMEasting;
                string Zone;
                Helper.ConvertLatLongtoUTM(gp.Latitude, gp.Longitude, out UTMNorthing, out UTMEasting, out Zone);

                float x;
                float y;
                Helper.ConvertUTMToPixel(UTMNorthing, UTMEasting, out x, out y);

                Coordinate c = new Coordinate(x, y, gp.Time, (float)(UTMEasting), (float)(UTMNorthing));
                this.Coords.Add(c);
            }
            GpxStream.Close();
            this.AddMissingPoints();
        }

		// Korrigere mangelfuld gps data
        public void AddMissingPoints()
        {
            int counter = 0;
            while (counter < this.Coords.Count - 1)
            {
                if (this.Coords[counter].Time >= this.Coords[counter + 1].Time)
                {
                    this.Coords.RemoveAt(counter + 1);
                    counter--;
                }
                else if (this.Coords[counter].Time.AddSeconds(1) != this.Coords[counter + 1].Time)
                {
                    Coordinate temp = this.Coords[counter];
                    temp.Time = temp.Time.AddSeconds(1);
                    this.Coords.Insert(counter + 1, temp);
                }
                counter++;
            }
        }
    }
}
