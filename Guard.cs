using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    ///<summary>
    /// Represents a Guard object that occupies a single x,y cell on the map.
    /// </summary>
    public class Guard : MapObject
    {
        /// <summary>
        /// Constructs a Guard which will occupy the given x and y coordinates. 
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public Guard(int x, int y) : base(x, y)
        {
            Console.WriteLine("Successfully added guard obstacle.");
        }

        /// <summary>
        /// This method takes an X and Y coordinate and will return true if
        /// the guard has been placed at that location.
        /// </summary>
        /// <param name="x"> The X coordinate to check.</param>
        /// <param name="y"> The Y coordinate to check.</param>
        /// <returns></returns>
        public bool IsOccupied(int x, int y)
        {
            //Check if this Guards coordinates match the provided X and Y coordinates.
            if (this.X == x && this.Y == y)
            {
                return true;
            }

            //Otherwise the Guard must not be at that location.
            return false;
        }
    }
}

