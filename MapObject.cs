using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    /// <summary>
    /// MapObject is an abstract class responsible for anything
    /// that can be placed on the map. E.g, Guard, Fence, Sensor, Camera.
    /// </summary>
    public abstract class MapObject : Map
    {
        //Private fields

        /// <summary>
        /// The default direction for a MapObject.
        /// </summary>
        private const char Default_Direction = 'N';

        /// <summary>
        /// The default length of a MapObject.
        /// </summary>
        private const int Default_Length = 0;

        /// <summary>
        /// The default range of a MapObject.
        /// </summary>
        private const float Default_Range = 0;

        /// <summary>
        /// The X coordinate of the MapObject.
        /// </summary>
        private int x;

        /// <summary>
        /// The Y coordinate of the MapObject.
        /// </summary>
        private int y;

        /// <summary>
        /// The type of obstacle, represented by a single char, e.g, 'G' for Guard.
        /// This is used for printing obstacle symbols to the map.
        /// </summary>
        private char obstacleType;

        /// <summary>
        /// The MapObject's length expressed as an int, used for the Fence.
        /// </summary>
        private int length = Default_Length;

        /// <summary>
        /// The MapObject's direction expressed as a char (N, S, E, W), used for the Fence and Camera.
        /// </summary>
        private char direction = Default_Direction;

        /// <summary>
        /// The MapObject's range expressed as a float, used for the Sensor.
        /// </summary>
        private float range = Default_Range;


        //External access to private fields is controlled through public getter and setter

        /// <summary>
        /// The X coordinate of the MapObject.
        /// </summary>
        public int X { get { return x; } private set { x = value; } }

        /// <summary>
        /// The Y coordinate of the MapObject.
        /// </summary>
        public int Y { get { return y; } private set { y = value; } }

        /// <summary>
        /// The type of obstacle, represented by a single char, e.g, 'G' for Guard.
        /// This is used for printing obstacle symbols to the map.
        /// </summary>
        public char ObstacleType { get { return obstacleType; } private set { obstacleType = value; } }

        /// <summary>
        /// The MapObject's direction expressed as a char (N, S, E, W), used for the Fence and Camera.
        /// </summary>
        public char Direction { get { return direction; } private set { direction = value; } }

        /// <summary>
        /// The MapObject's length expressed as an int, used for the Fence.
        /// </summary>
        public int Length { get { return length; } private set { length = value; } }

        /// <summary>
        /// The MapObject's range expressed as a float, used for the Sensor.
        /// </summary>
        public float Range { get { return range; } private set { range = value; } }


        /// <summary>
        /// A MapObject constructor for the Guard class. Requires an X and Y coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public MapObject(int x, int y)
        {
            X = x;
            Y = y;
            ObstacleType = 'G';
        }

        /// <summary>
        /// A MapObject constructor for the Fence class. Requires an X and Y coordinate, 
        /// a direction (east or north), and a length.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="direction">The obstacle direction (N or E).</param>
        /// <param name="length">The obstacle length as an int.</param>
        public MapObject(int x, int y, char direction, int length)
        {
            X = x;
            Y = y;
            ObstacleType = 'F';
            Direction = direction;
            Length = length;
        }

        /// <summary>
        /// A MapObject constructor for the Sensor class. Requires an X and Y coordinate,
        /// and a range as a float.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="range">The range of the Sensor, expressed as a floating point number.</param>
        public MapObject(int x, int y, float range)
        {
            X = x;
            Y = y;
            ObstacleType = 'S';
            Range = range;
        }

        /// <summary>
        /// A MapObject constructor for the Camera class. Requires an X and Y coordinate,
        /// and a direction (N, S, E, or W).
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="direction">The direction faced (N, S, E, or W).</param>
        public MapObject(int x, int y, char direction)
        {
            X = x;
            Y = y;
            ObstacleType = 'C';
            Direction = direction;
        }
    }
}
