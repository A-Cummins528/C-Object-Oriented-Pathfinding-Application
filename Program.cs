using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;
using System.IO;
using System.Security.AccessControl;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    /// <summary>
    /// This is the object avoidance systems main loop
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initalise the game map and print welcome message.
            Map myMap = new Map();
            Console.WriteLine("Welcome to my object avoidance simulation, I hope you enjoy it!");
            Utility.Help();

            //Begin the main menu loop.
            bool running = true;
            while (running)
            {
                //Prompt the user for input and split the input into an array of commands.
                Console.WriteLine("Enter command:");
                string input = Console.ReadLine() ?? "";
                string[] commands = input.Split(' ');
                try
                {
                    //If the first word is "add".
                    if (commands[0] == "add")
                    {
                        //Check the total length of the command is more than a single word.
                        if (commands.Length == 1)
                        {
                            Console.WriteLine("You need to specify an obstacle type.");
                            continue;
                        }

                        //Check if the second word is not an obstacle type.
                        if ((commands[1] != "guard")
                            && (commands[1] != "fence")
                            && (commands[1] != "sensor")
                            && (commands[1] != "camera"))
                        {
                            Console.WriteLine("Invalid obstacle type.");
                            continue;
                        }

                        //If the command is "add guard", begin the logic to add a guard obstacle.
                        if (commands[1] == "guard")
                        {
                            try
                            {
                                //Check the length of the command is correct.
                                if (commands.Length != 4)
                                {
                                    throw new ArgumentException("Incorrect number of arguments.");
                                }

                                //Get the X and Y coordinate for the Guard.
                                int x = int.Parse(commands[2]);
                                int y = int.Parse(commands[3]);

                                //Command is correct and complete, construct the Guard and add it to the list of objects.
                                Guard guard = new Guard(x, y);
                                myMap.mapObjects.Add(guard);
                                continue;
                            }

                            //Check the coordinates provided are valid ints.
                            catch (FormatException)
                            {
                                Console.WriteLine("Coordinates are not valid integers.");
                                continue;
                            }

                            //If the length of the command is not correct.
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }
                        }

                        //If the command is "add fence", begin the logic to add a fence obstacle.
                        if (commands[1] == "fence")
                        {
                            try
                            {
                                //Check that the command length is correct.
                                if (commands.Length != 6)
                                {
                                    throw new ArgumentException("Incorrect number of arguments.");
                                }

                                //Get the starting X and Y coordinate for the Fence
                                int x = int.Parse(commands[2]);
                                int y = int.Parse(commands[3]);

                                //Check that the fence direction is north or south.
                                if (commands[4] != "east" && commands[4] != "north")
                                {
                                    throw new ArgumentException("Orientation must be 'east' or 'north'.");
                                }

                                //Declare my direction variable then assign it the char E or N.
                                char direction;

                                if (commands[4] == "east")
                                {
                                    direction = 'E';
                                }

                                else
                                {
                                    direction = 'N';
                                }

                                //Check that the provided length is a valid int, not less than or equal to 0.
                                if (!int.TryParse(commands[5], out int length) || length <= 0)
                                {
                                    throw new ArgumentException("Length must be a valid integer greater than 0.");
                                }

                                //Command is complete and correct, construct the Fence and add it to the list of objects.
                                Fence fence = new Fence(x, y, direction, length);
                                myMap.mapObjects.Add(fence);
                                continue;
                            }

                            //If the length of the command, or the Fence length or orientation is not correct.
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }

                            //Check the coordinates provided are valid ints.
                            catch (FormatException)
                            {
                                Console.WriteLine("Coordinates are not valid integers.");
                                continue;
                            }
                        }

                        //If command is "add sensor".
                        if (commands[1] == "sensor")
                        {
                            try
                            {
                                //Check that the length of the command is correct.
                                if (commands.Length != 5)
                                {
                                    throw new ArgumentException("Incorrect number of arguments.");
                                }

                                //Get the starting X and Y coordinate for the Sensor.
                                int x = int.Parse(commands[2]);
                                int y = int.Parse(commands[3]);

                                //Check that the range is valid.
                                if (!float.TryParse(commands[4], out float range) || range <= 0)
                                {
                                    throw new ArgumentException("Range must be a valid positive number.");
                                }

                                //The command is complete and correct, construct the Sensor and add it to the list of objects.
                                Sensor sensor = new Sensor(x, y, range);
                                myMap.mapObjects.Add(sensor);
                                continue;
                            }

                            //If the command length is not correct or the Sensor range is not valid
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }

                            //Check the coordinates provided are valid ints.
                            catch (FormatException)
                            {
                                Console.WriteLine("Coordinates are not valid integers.");
                                continue;
                            }
                        }

                        //If command is "add camera".
                        if (commands[1] == "camera")
                        {
                            try
                            {
                                //Check command length is correct.
                                if (commands.Length != 5)
                                {
                                    throw new ArgumentException("Incorrect number of arguments.");
                                }

                                //Get the starting X and Y coordinate for the Camera.
                                int x = int.Parse(commands[2]);
                                int y = int.Parse(commands[3]);

                                //Initalise the direction variable.
                                char direction;

                                //Check that the input direction is valid, and if it is, assign it to the direction variable.
                                if (commands[4] != "north" && commands[4] != "south" && commands[4] != "east" && commands[4] != "west")
                                {
                                    throw new ArgumentException("Direction must be 'north', 'south', 'east' or 'west'.");
                                }

                                if (commands[4] == "north")
                                {
                                    direction = 'N';
                                }

                                else if (commands[4] == "south")
                                {
                                    direction = 'S';
                                }

                                else if (commands[4] == "east")
                                {
                                    direction = 'E';
                                }

                                else
                                {
                                    direction = 'W';
                                }

                                //Command is complete and correct, construct the Camera and add it to the list of map objects.
                                Camera camera = new Camera(x, y, direction);
                                myMap.mapObjects.Add(camera);
                                continue;
                            }

                            //If the command length or Camera direction is not correct 
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }

                            //Check the coordinates provided are valid ints.
                            catch (FormatException)
                            {
                                Console.WriteLine("Coordinates are not valid integers.");
                                continue;
                            }
                        }
                    }

                    //Relates to checking if a coordinate and it's neighbours are safe to travel.
                    if (commands[0] == "check")
                    {
                        try
                        {
                            //Check the length of the command.
                            if (commands.Length != 3)
                            {
                                throw new ArgumentException("Incorrect number of arguments.");
                            }

                            //Get the X and Y coordinate to check
                            int x = int.Parse(commands[1]);
                            int y = int.Parse(commands[2]);

                            //If the cell is safe.
                            if (Map.IsCoordSafe(x, y))
                            {
                                //Check neighbour cells.
                                myMap.CheckDirection(x, y);
                                continue;
                            }

                            //Otherwise the cell is not safe.
                            else
                                Console.WriteLine("Agent, your location is compromised. Abort mission.");
                            continue;
                        }

                        //If the command length is not correct 
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                        //Check the coordinates provided are valid ints.
                        catch (FormatException)
                        {
                            Console.WriteLine("Coordinates are not valid integers.");
                            continue;
                        }
                    }

                    //This relates to drawing a map on the screen
                    if (commands[0] == "map")
                    {
                        try
                        {
                            //Check command length is correct.
                            if (commands.Length != 5)
                            {
                                throw new ArgumentException("Incorrect number of arguments.");
                            }

                            //Get the X and Y coordinate of the map's bottom left corner
                            int x = int.Parse(commands[1]);
                            int y = int.Parse(commands[2]);

                            //Check width and height are correct.
                            if (!int.TryParse(commands[3], out int width) || !int.TryParse(commands[4],
                                out int height) || width <= 0 || height <= 0)
                            {
                                throw new ArgumentException("Width and height must be valid positive integers.");
                            }

                            //Command is complete and correct, draw the map on the screen by calling the DisplayMap method.
                            Console.WriteLine("Here is a map of obstacles in the selected region:");
                            myMap.DisplayMap(x, y, width, height);
                            continue;
                        }

                        //If the command length, the width or the height is not correct 
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                        //Check the coordinates provided are valid ints.
                        catch (FormatException)
                        {
                            Console.WriteLine("Coordinates are not valid integers.");
                            continue;
                        }
                    }

                    //Relates to pathfinding from a starting position to a goal position.
                    if (commands[0] == "path")
                    {
                        try
                        {
                            //Check command length is correct.
                            if (commands.Length != 5)
                            {
                                throw new ArgumentException("Incorrect number of arguments.");
                            }

                            //Get the starting coordinate.
                            int start_x = int.Parse(commands[1]);
                            int start_y = int.Parse(commands[2]);

                            //Check if goal coordinate is correct.
                            if (!int.TryParse(commands[3], out int goal_x) || !int.TryParse(commands[4], out int goal_y))
                            {
                                throw new ArgumentException("Objective coordinates are not valid integers.");
                            }

                            //If coordinates are the same.
                            if (start_x == goal_x && start_y == goal_y)
                            {
                                Console.WriteLine("Agent, you are already at the objective.");
                                continue;
                            }

                            //Check if the goal coordinate is safe.
                            if (!Map.IsCoordSafe(goal_x, goal_y))
                            {
                                Console.WriteLine("The objective is blocked by an obstacle and cannot be reached.");
                                continue;
                            }

                            //Otherwise, generate the path with FindPath method using the provided ints
                            Path.FindPath(start_x, start_y, goal_x, goal_y);
                            continue;
                        }

                        //If the command length or the objective coordinates are not valid
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                        //Check the starting coordinates provided are valid ints.
                        catch (FormatException)
                        {
                            Console.WriteLine("Agent coordinates are not valid integers.");
                            continue;
                        }
                    }

                    //If command is "help"
                    if (commands[0] == "help")
                    {
                        //Run utility help command.
                        Utility.Help();
                        continue;
                    }

                    //If user wishes to exit the program.
                    if (commands[0] == "exit")
                    {
                        Console.WriteLine("Thank you for using my program!");
                        running = false;
                        break;
                    }

                    else
                    {
                        throw new ArgumentException($"Invalid option: {input}");
                    }
                }
                
                //Otherwise the menu input is invalid, return the input and prompt the user.
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Type 'help' to see a list of commands.");
                    continue;
                }
            }
        }
    }
}
