using System;
using System.Collections.Generic;
using System.Text;

namespace XplaneAirportParser.Data
{
	class Airport
	{
		public enum SurfaceTypes
		{
			Asphalt = 1,
			Concrete = 2,
			Grass = 3,
			Dirt = 4,
			Gravel = 5,
			DryLakebed = 12,
			Water = 13,
			SnowOrIce = 14,
			Other = 15,
			Undefined = 999
		}



		public string ICAO;

		public string country;

		public string city;

		public string AirportName;

		public float latitude;

		public float longitude;

		public float runwayLength;

		public SurfaceTypes surfaceType = SurfaceTypes.Undefined;

		public bool hasProperLatLon;


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

			if (surfaceType == SurfaceTypes.Undefined)
				return false;

			return true;
		}

		public override string ToString()
		{
			return "ICAO: " + ICAO + " Name: " + AirportName + " City: " + city + " Country: " + country + " Lat: " + latitude + " Lon: " + longitude + " Length: " + runwayLength + "	Type: " + surfaceType;
		}
	}
}
