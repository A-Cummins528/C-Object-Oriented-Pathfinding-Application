using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    /// <summary>
    /// Represents a camera objectthat observes a cone shaped area of the map, 
    /// extending outward infinitely from the starting cell.
    /// </summary>
    public class Camera : MapObject
    {
        /// <summary>
        /// Constructs a new camera which will observe cells in the specified direction (N, S, E, or W)
        /// from the given x and y coordinates.
        /// </summary>
        public Camera(int x, int y, char direction) : base(x, y, direction)
        {
            Console.WriteLine("Successfully added camera obstacle.");
        }

        /// <summary>
        /// This method takes an X and Y coordinate and will return true if
        /// that location falls within the Cameras observable area.
        /// </summary>
        /// <param name="x">The X coordinate to check.</param>
        /// <param name="y">The Y coordinate to check.</param>
        /// <returns></returns>
        public bool IsOccupied(int x, int y)
        {
            //First check to see if the Camera was placed on the given coordinates.
            if (this.X == x && this.Y == y)
            {
                return true;
            }

            //Calculate the absolute X and Y values which will be used to determine if the point
            //lies within the infinite triangular observable area.
            float AbsX = (Math.Abs(x - this.X));
            float AbsY = (Math.Abs(y - this.Y));



            //If Camera direction is North.
            if (this.Direction == 'N')
            {
                //If the point is within the observable area.
                if (y >= this.Y && (AbsX / 2) <= (AbsY / 2))
                {
                    return true;
                }
            }

            //If Camera direction is South.
            if (this.Direction == 'S')
            {
                //If the point is within the observable area.
                if (y <= this.Y && (AbsX / 2) <= (AbsY / 2))
                {
                    return true;
                }
            }

            //If Camera direction is East.
            if (this.Direction == 'E')
            {
                //If the point is within the observable area.
                if (x >= this.X && (AbsX / 2) >= (AbsY / 2))
                {
                    return true;
                }
            }

            //If Camera direction is West.
            if (this.Direction == 'W')
            {
                //If the point is within the observable area.
                if (x <= this.X && (AbsX / 2) >= (AbsY / 2))
                {
                    return true;
                }
            }

            //Otherwise the coordinates must not lie within the observable area
            return false;
        }
    }
}
