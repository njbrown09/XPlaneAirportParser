using System;
using System.Collections.Generic;
using System.Text;

namespace XplaneAirportParser.Data
{
	class Airport
	{
		
		public string ICAO;

		public string country;

		public string city;

		public string AirportName;

		public float latitude;

		public float longitude;

		public float runwayLength;

		public int elevation;

		public bool hasProperLatLon;

        public List<Runway> runways = new List<Runway>();

		/// <summary>
		/// Is the airport data filled (is it done and ready for processing?)
		/// </summary>
		/// <returns>True if the airport has completed being parsed</returns>
		public bool isFilled()
		{
			if (ICAO == null)
				return false;

			if (AirportName == null)
				return false;

			if (latitude == float.NaN)
				return false;

			if (longitude == float.NaN)
				return false;

			if (runwayLength == float.NaN)
				return false;

			if (country == null)
				return false;

			if (city == null)
				return false;

			if (elevation == null)
				return false;


			return true;
		}

		public override string ToString()
		{
			return "ICAO: " + ICAO + " Name: " + AirportName + " City: " + city + " Country: " + country + " Lat: " + latitude + " Lon: " + longitude + "	Elv: " + elevation;
		}
	}
}
