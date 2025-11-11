using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    /// <summary>
    /// The Map class is responsible for drawing the Map and checking Map cells.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// A private static list of Objects which have been placed on the map.
        /// </summary>
        private static List<MapObject> MapObject_List = new List<MapObject>();

        /// <summary>
        /// A public list of objects which have been placed on the map.
        /// </summary>
        public List<MapObject> mapObjects { get { return MapObject_List; } }

        /// <summary>
        /// The default map constructor.
        /// </summary>
        public Map()
        {

        }


        /// <summary>
        /// This draws a 2D representation of the specified area of the map using the
        /// provided width and height, with the X and Y coordinate representing the bottom left corner of the map.
        /// Obstacles will appear as their corresponding char, e.g., 'G', and blank cells will be represented
        /// by a '.'
        /// </summary>
        /// <param name="x">The bottom left X coordinate.</param>
        /// <param name="y">The bottom left Y coordinate.</param>
        /// <param name="width">How many cells wide the map should be.</param>
        /// <param name="height">The height of the map.</param>
        public void DisplayMap(int x, int y, int width, int height)
        {
            //Check each cell, row by row, starting from the map's top left cell.
            for (int j = y + height - 1; j >= y; j--)
            {
                for (int i = 0; i < width; i++)
                {
                    //If the cell is not safe.
                    if (!IsCoordSafe(x + i, j))
                    {
                        //Check the list of map objects and draw the corresponding obstacle char for that cell.
                        foreach (var obj in MapObject_List)
                        {
                            if (obj is Guard guard)
                            {
                                if (guard.IsOccupied(x + i, j))
                                {
                                    Console.Write(guard.ObstacleType);
                                    break;
                                }
                            }

                            if (obj is Fence fence)
                            {
                                if (fence.IsOccupied(x + i, j))
                                {
                                    Console.Write(fence.ObstacleType);
                                    break;
                                }
                            }

                            if (obj is Sensor sensor)
                            {
                                if (sensor.IsOccupied(x + i, j))
                                {
                                    Console.Write(sensor.ObstacleType);
                                    break;
                                }
                            }

                            else if (obj is Camera camera)
                            {
                                if (camera.IsOccupied(x + i, j))
                                {
                                    Console.Write(camera.ObstacleType);
                                    break;
                                }
                            }
                        }
                    }

                    //Otherwise the cell must be safe/empty.
                    else
                    {
                        Console.Write('.');
                    }
                }
                //Begin writing the next row of the map.
                Console.WriteLine();
            }
        }

        /// <summary>
        /// This method prints which direction, if any, is safe to travel from the given coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns></returns>
        public void CheckDirection(int x, int y)
        {
            //Initalise four safe directions.
            bool North = true;
            bool South = true;
            bool East = true;
            bool West = true;

            //Check the four directions around the coordinate cell (N, S, E, W)
            //and if that neighbouring cell is not safe, flag the direction bool as false.
            if (!IsCoordSafe(x + 1, y))
            {
                East = false;
            }

            if (!IsCoordSafe(x - 1, y))
            {
                West = false;
            }

            if (!IsCoordSafe(x, y + 1))
            {
                North = false;
            }

            if (!IsCoordSafe(x, y - 1))
            {
                South = false;
            }

            // If there is no safe direction
            if (!North && !South && !East && !West)
            {
                Console.WriteLine("You cannot safely move in any direction. Abort mission.");
            }

            //Otherwise print whichever direction is still flagged as safe (true).
            else
            {
                Console.WriteLine("You can safely take any of the following directions:");
                if (North) Console.WriteLine("North");
                if (South) Console.WriteLine("South");
                if (East) Console.WriteLine("East");
                if (West) Console.WriteLine("West");
            }
        }

        /// <summary>
        /// Checks if a given coordinate is safe. If the coordinate is not
        /// occupied or observed by an object, this method will return true.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns></returns>
        public static bool IsCoordSafe(int x, int y)
        {
            //Check each obstacle in the object list.
            //If an obstacle observes/occupies that coordinate, flag coordinate as not safe by returning false.
            foreach (var obj in MapObject_List)
            {
                if (obj is Guard guard)
                {
                    if (guard.IsOccupied(x, y))
                    {
                        return false;
                    }
                }

                if (obj is Fence fence)
                {
                    if (fence.IsOccupied(x, y))
                    {
                        return false;
                    }
                }

                if (obj is Sensor sensor)
                {
                    if (sensor.IsOccupied(x, y))
                    {
                        return false;
                    }
                }

                if (obj is Camera camera)
                {
                    if (camera.IsOccupied(x, y))
                    {
                        return false;
                    }
                }
            }

            //Otherwise, the coordinates do not match anything in the object list and can be considered safe.
            return true;
        }
    }
}

