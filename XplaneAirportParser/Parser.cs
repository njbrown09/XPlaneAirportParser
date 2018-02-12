using System;
using System.Collections.Generic;
using System.Text;
using XplaneAirportParser.Data;
using System.Device.Location;

namespace XplaneAirportParser
{
	public class Parser
	{
		public const string AIRPORT_PREFIX = "1";

		public const string METADATA_PREFIX = "1302";

		public const string RUNWAY_PREFIX = "100";

		public const string VIEWPORT_PREFIX = "14";

		/// <summary>
		/// Starts the parsing, dont call manually
		/// </summary>
		public void StartParsing()
		{
			string[] Lines = System.IO.File.ReadAllLines("apt.dat");

			Airport CurrentAiport = new Airport();

			foreach (string line in Lines)
			{

				if (string.IsNullOrWhiteSpace(line)) continue; //If empty, skip

				string[] segments = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                switch (segments[0])
                {
                    case AIRPORT_PREFIX:
                        if (CurrentAiport.isFilled()) //If the airport is done handle it
                        {
                            HandleAirport(CurrentAiport);
                        }
                        CurrentAiport = new Airport(); //Create a new airport
                        CurrentAiport.ICAO = segments[4]; //Set the airports ICAO
                        CurrentAiport.elevation = int.Parse(segments[1]);

                        //Processes the name of the airport
                        for (int i = 0; i < segments.Length; i++)
                        {
                            if (i == 5)
                            {
                                CurrentAiport.AirportName = segments[5];
                            }
                            else
                            {
                                CurrentAiport.AirportName += " " + segments[i];
                            }
                        }
                        break;

                    case RUNWAY_PREFIX:
                        //Get coords of start and end of runway
                        var coord1 = new GeoCoordinate(double.Parse(segments[9]), double.Parse(segments[10]));
                        var coord2 = new GeoCoordinate(double.Parse(segments[18]), double.Parse(segments[19]));

                        float runwayLength = ((float)coord1.GetDistanceTo(coord2) * 3.28084f); //3.28084f is the meters to feet conversion!

                        //Add runway to airport runways
                        CurrentAiport.runways.Add(new Runway(float.Parse(segments[9]), float.Parse(segments[10]), float.Parse(segments[18]), float.Parse(segments[19]), runwayLength, "NYI", (Runway.SurfaceTypes)int.Parse(segments[2])));

                        //9, 10
                        if (!CurrentAiport.hasProperLatLon)
                        {
                            Runway LongestRunway = new Runway(0, 0, 0, 0, 0, null, Runway.SurfaceTypes.Undefined);
                            foreach (Runway rwy in CurrentAiport.runways)
                            {
                                if (rwy.length > LongestRunway.length)
                                {
                                    LongestRunway = rwy;
                                }
                            }
                            CurrentAiport.latitude = (LongestRunway.startLat + LongestRunway.endLat) / 2f;
                            CurrentAiport.longitude = (LongestRunway.startLon + LongestRunway.endLon) / 2f;
                            CurrentAiport.hasProperLatLon = true;
                        }
                        break;

                    case VIEWPORT_PREFIX:
                        if (!CurrentAiport.hasProperLatLon)
                        {
                            CurrentAiport.latitude = float.Parse(segments[1]);
                            CurrentAiport.longitude = float.Parse(segments[2]);
                            CurrentAiport.hasProperLatLon = true;
                        }
                        break;

                    case METADATA_PREFIX:
                        switch (segments[1])
                        {
                            case "city":
                                try
                                {
                                    CurrentAiport.city = segments[2];
                                    for (int i = 3; i < segments.Length; i++)
                                    {
                                        CurrentAiport.city += " " + segments[i];
                                    }
                                }
                                catch { }
                                break;

                            case "state":
                                try
                                {
                                    CurrentAiport.state = segments[2];
                                    for (int i = 3; i < segments.Length; i++)
                                    {
                                        CurrentAiport.state += " " + segments[i];
                                    }
                                }
                                catch { }
                                break;

                            case "country":
                                try
                                {
                                    CurrentAiport.country = segments[2];
                                    for (int i = 3; i < segments.Length; i++)
                                    {
                                        CurrentAiport.country += " " + segments[i];
                                    }
                                }
                                catch { }
                                break;

                            case "datum_lat":
                                try
                                {
                                    CurrentAiport.latitude = float.Parse(segments[2]);
                                    CurrentAiport.hasProperLatLon = true;
                                }
                                catch { }
                                break;

                            case "datum_lon":
                                try
                                {
                                    CurrentAiport.longitude = float.Parse(segments[2]);
                                    CurrentAiport.hasProperLatLon = true;
                                }
                                catch { }
                                break;
                        }
                        break;
                }
            }
            while (true) { }
        }

		/// <summary>
		/// Gets called whenever an airport is parsed, but your stuff you want to use the airport stuff with (Like a db insert Query!)
		/// </summary>
		/// <param name="airport">Parsed Airport</param>
		public virtual void HandleAirport(Airport airport)
		{
			Console.WriteLine(airport);
            foreach (Runway runway in airport.runways)
                Console.WriteLine(runway);
		}
	}
}
