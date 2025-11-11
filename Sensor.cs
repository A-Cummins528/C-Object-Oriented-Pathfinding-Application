using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    /// <summary>
    /// Represents a sensor object which observes a circular area of the map,
    /// extending outward from the initial coordinates at a distance which depends on the range given.
    /// </summary>
    public class Sensor : MapObject
    {
        /// <summary>
        /// Constructs a new sensor at the given x and y coordinates, which will observe a circular area
        /// at a distance which is dependant on the range given.
        /// </summary>
        public Sensor(int x, int y, float range) : base(x, y, range)
        {
            Console.WriteLine("Successfully added sensor obstacle.");
        }

        /// <summary>
        /// This method takes an X and Y coordinate and will return true if
        /// that location falls within the Sensors detectable area.
        /// </summary>
        /// <param name="x">The X coordinate to check.</param>
        /// <param name="y">The Y coordinate to check.</param>
        /// <returns></returns>
        public bool IsOccupied(int x, int y)
        {
            //First check to see if the Sensor was placed on the given coordinates.
            if (this.X == x && this.Y == y)
            {
                return true;
            }

            //Caculate the distance between the Sensors starting location and the supplied coordinates.
            float fx = x - this.X;
            float fy = y - this.Y;
            double DistanceFromSensor = Math.Sqrt(fx * fx + fy * fy);

            //Check if the distance is less than the Sensor range. If it is, return true.
            if (DistanceFromSensor <= this.Range)
            {
                return true;
            }

            //Otherwise the coordinate does not fall within the range of the Sensor.
            return false;
        }
    }
}
