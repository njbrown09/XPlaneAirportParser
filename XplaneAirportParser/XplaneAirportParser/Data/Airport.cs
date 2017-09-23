using System;
using System.Collections.Generic;
using System.Text;

namespace XplaneAirportParser.Data
{
	struct Airport
	{
		public string ICAO;

		public string country;

		public string city;

		public string AirportName;

		public float latitude;

		public float longitude;

		public float runwayLength;

		public bool hasProperLatLon;


		/// <summary>
		/// Is the airport data filled (is it done and ready for processing?)
		/// </summary>
		/// <returns>True if you should process this data</returns>
		public bool isFilled()
		{
			bool isFilled = true;

			if (ICAO == null)
				isFilled = false;

			if (AirportName == null)
				isFilled = false;

			if (latitude == null)
				isFilled = false;

			if (longitude == null)
				isFilled = false;

			if (runwayLength == null)
				isFilled = false;

			if (country == null)
				isFilled = false;

			if (city == null)
				isFilled = false;


			return isFilled;
		}

		public string ToString()
		{
			return "ICAO: " + ICAO + " Name: " + AirportName + " City: " + city + " Country: " + country + " Lat: " + latitude + " Lon: " + longitude + " Length: " + runwayLength;
		}
	}
}
