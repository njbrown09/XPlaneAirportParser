using System;
using System.Device.Location;
using System.Collections.Generic;
using System.Text;
using XplaneAirportParser.Data;

namespace XplaneAirportParser
{
	class Parser
	{
		public const string AIRPORT_PREFIX = "1";

		public const string METADATA_PREFIX = "1302";

		public const string RUNWAY_PREFIX = "100";

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


				if (segments[0] == AIRPORT_PREFIX) //If the prefix is a new airport prefix
				{

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
				}
				else if (segments[0] == RUNWAY_PREFIX)
				{
					//9, 10
					if (!CurrentAiport.hasProperLatLon)
					{
						CurrentAiport.latitude = float.Parse(segments[9]);
						CurrentAiport.longitude = float.Parse(segments[10]);
					}

					if (int.Parse(segments[2]) < (int)CurrentAiport.surfaceType)
					{
						CurrentAiport.surfaceType = (Airport.SurfaceTypes)int.Parse(segments[2]);
					}
					var coord1 = new GeoCoordinate(double.Parse(segments[9]), double.Parse(segments[10]));
					var coord2 = new GeoCoordinate(double.Parse(segments[18]), double.Parse(segments[19]));


					CurrentAiport.runwayLength = ((float)coord1.GetDistanceTo(coord2) * 3.28084f); //3.28084f is the meters to feet conversion!
				}
				else if (segments[0] == METADATA_PREFIX)
				{
					if (segments[1] == "datum_lat")
					{
						try
						{
							CurrentAiport.latitude = float.Parse(segments[2]);
							CurrentAiport.hasProperLatLon = true;
						}
						catch { CurrentAiport.hasProperLatLon = false; }
					}
					else if (segments[1] == "datum_lon")
					{
						try
						{
							CurrentAiport.longitude = float.Parse(segments[2]);
							CurrentAiport.hasProperLatLon = true;
						}
						catch { CurrentAiport.hasProperLatLon = false; }
					}

					if (segments[1] == "city")
					{
						try
						{
							CurrentAiport.city = segments[2];
							for (int i = 3; i < segments.Length; i++)
							{
								CurrentAiport.city += " " + segments[i];
							}
						}
						catch { }
					}
					if (segments[1] == "country")
					{
						try
						{
							CurrentAiport.country = segments[2];
							for (int i = 3; i < segments.Length; i++)
							{
								CurrentAiport.country += " " + segments[i];
							}
						}
						catch { }
					}
				}
			}
		}

		/// <summary>
		/// Gets called whenever an airport is parsed, but your stuff you want to use the airport stuff with (Like a db insert Query!)
		/// </summary>
		/// <param name="airport">Parsed Airport</param>
		public void HandleAirport(Airport airport)
		{
			Console.WriteLine(airport);
		}
	}
}
