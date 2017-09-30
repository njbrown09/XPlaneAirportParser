using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XplaneAirportParser.Data
{
    class Runway
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

        public SurfaceTypes surfaceType;

        public float startLat;

        public float startLon;

        public float endLat;

        public float endLon;

        public float length;

        public string runwayId;

        public Runway(float startLat, float startLon, float endLat, float endLon, float length, string runwayId, SurfaceTypes surfaceType)
        {
            this.startLat = startLat;
            this.startLon = startLon;
            this.endLat = endLat;
            this.endLon = endLon;
            this.length = length;
            this.runwayId = runwayId;
            this.surfaceType = surfaceType;
        }

        public override string ToString()
        {
            return "-- Length: " + length + " Surface: " + surfaceType;
        }
    }
}
