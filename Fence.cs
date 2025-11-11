using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    /// <summary>
    /// Represents a Fence object that occupies either a horizontal or vertical area of the map, 
    /// with a width of one cell and a length which is specified when constructed. 
    /// </summary>
    public class Fence : MapObject
    {
        /// <summary>
        /// Constructs a Fence of the specified length, running along the specified direction (north or east),
        /// starting at the given x and y coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="orientation">The orientation of the Fence (north or east).</param>
        /// <param name="length">How many cells long the Fence is.</param>
        public Fence(int x, int y, char orientation, int length) : base(x, y, orientation, length)
        {
            Console.WriteLine("Successfully added fence obstacle.");
        }

        /// <summary>
        /// This method takes an X and Y coordinate and will return true if
        /// that location falls within the area occupied by the Fence.
        /// </summary>
        /// <param name="x">The X coordinate to check.</param>
        /// <param name="y">The Y coordinate to check.</param>
        /// <returns></returns>
        public bool IsOccupied(int x, int y)
        {
            //First check to see if the Fence was constructed on the given coordinates.
            if (this.X == x && this.Y == y)
            {
                return true;
            }

            //If the Fence is running North.
            if (this.Direction == 'N')
            {
                //Calculate the last Y coordinate occupied by this fence.
                int LastY = this.Y + this.Length - 1;

                //Compare the given coordinate to the known length and location of the Fence.
                if (this.X == x && this.Y <= y && y <= LastY)
                {
                    return true;
                }
            }

            //If the Fence is running East.
            if (this.Direction == 'E')
            {
                //Calculate the last X coordinate occupied by this Fence.
                int LastX = this.X + this.Length - 1;

                //Compare the given coordinate to the known length and location of the Fence.
                if (this.Y == y && this.X <= x && x <= LastX)
                {
                    return true;
                }
            }

            //Otherwise, the given coordinates must not intersect with the Fence object
            return false;
        }
    }
}
