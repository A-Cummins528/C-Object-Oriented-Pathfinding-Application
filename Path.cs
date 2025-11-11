using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    //References
    //Resources used to help understand A* pathfinding:
    //https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
    //https://robotics.caltech.edu/wiki/images/e/e0/Astar.pdf
    //https://www.youtube.com/watch?v=i0x5fj4PqP4

    /// <summary>
    /// Responsible for the pathfinding algorithm
    /// </summary>
    public class Path : Map
    {
        //Non-const fields are always private.

        /// <summary>
        /// The starting X coordinate.
        /// </summary>
        private int x;

        /// <summary>
        /// The starting Y coordinate.
        /// </summary>
        private int y;

        /// <summary>
        /// The target X coordinate.
        /// </summary>
        private int goal_x;

        /// <summary>
        /// The target Y coordinate.
        /// </summary>
        private int goal_y;

        //External access to private non-const fields is controlled through public getter and setter

        /// <summary>
        /// The starting X coordinate.
        /// </summary>
        public int Start_X { get { return x; } set { x = value; } }

        /// <summary>
        /// The starting Y coordinate.
        /// </summary>
        public int Start_Y { get { return y; } set { y = value; } }

        /// <summary>
        /// The target Y coordinate.
        /// </summary>
        public int Goal_Y { get { return goal_y; } set { goal_y = value; } }

        /// <summary>
        /// The target X coordinate.
        /// </summary>
        public int Goal_X { get { return goal_x; } set { goal_x = value; } }

        /// <summary>
        /// Returns a list of safe Nodes adjacent to the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns></returns>
        static List<Node> GetWalkableAdjacentSquares(int x, int y)
        {
            //Create a list of possibly safe Nodes, in the four directions around the provided coordinates (neighbours).
            var proposedLocations = new List<Node>()
            {
                new Node(x, y - 1),
                new Node(x, y + 1),
                new Node(x - 1, y),
                new Node(x + 1, y),
            };

            // Create the list of safe neighbour Nodes by filtering out neighbour Nodes that are not safe (occupied by obstacles)
            var walkableLocations = proposedLocations.Where(node => IsCoordSafe(node.X, node.Y)).ToList();

            //Return the list of safe neighbour Nodes.
            return walkableLocations;
        }

        /// <summary>
        /// A helper method which will compute a Node's H (Heuristic) score.
        /// Which is the estimated distance from this node to the end (goal) node
        /// </summary>
        /// <param name="x">This Node's X coordinate.</param>
        /// <param name="y">This Node's Y coordinate.</param>
        /// <param name="targetX">The goal X coordinate.</param>
        /// <param name="targetY">The goal Y coordinate.</param>
        /// <returns></returns>
        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            //Retrun the absolute distance between current node and target node (H).
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }

        /// <summary>
        /// Uses the A* pathfinding algorithm to print safe directions, if any, from the
        /// starting coordinate to the goal coordinate.
        /// </summary>
        /// <param name="start_x">The starting X coordinate.</param>
        /// <param name="start_y">The starting Y coordinate.</param>
        /// <param name="goal_x">The target X coordinate.</param>
        /// <param name="goal_y">The target Y coordinate.</param>
        public static void FindPath(int start_x, int start_y, int goal_x, int goal_y)
        {
            //Initalise a path object with the provided coordinates as properties.
            Path path = new Path();
            path.Start_X = start_x;
            path.Start_X = start_x;
            path.Start_Y = start_y;
            path.Goal_X = goal_x;
            path.Goal_Y = goal_y;

            //Create the goal Node. This is the Node we are working our way towards.
            Node goal_node = new Node(goal_x, goal_y);

            //Create the start node based on the provided initial coordinates.
            Node start_node = new Node(start_x, start_y);

            //Set current Node to the starting Node.
            Node current = start_node;

            //Create an open list used for neighbour Nodes which can be explored.
            var openList = new List<Node>();

            //Create a closed list of already visited Nodes.
            var closedList = new List<Node>();

            //Initalise the 'G' score as 0.
            //This represents the distance (how many steps) travelled from the starting node.
            int g = 0;

            //Add the start node to the open list and begin the pathfinding loop.
            openList.Add(start_node);

            //While open list is not empty
            while (openList.Count > 0)
            {
                //Pick the Node on the open list with the lowest F (G + H) score, and set current to that Node.
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                //Add the current node to the closed list and remove it from the open list.
                closedList.Add(current);
                openList.Remove(current);

                //If we added the goal node to the closed list, the path has been found.
                if (current.X == goal_x && current.Y == goal_y)
                {
                    Console.WriteLine("The following path will take you to the objective:");

                    //Create the list of Nodes to reach the goal by backtracking the Nodes visited.
                    List<Node> coordinateList = new List<Node>();

                    //Until we run out of parent nodes.
                    while (current != null)
                    {
                        //Add the Node to the path list and set current Node to be the previous Node's parent.
                        coordinateList.Add(current);
                        current = current.Parent;
                    }

                    //Reverse the list, so that it provides directions from the starting point to the goal.
                    coordinateList.Reverse();

                    //Print the path by giving the list to the helper method.
                    PrintDirection(coordinateList);
                    break;
                }

                //Otherwise we are not at the goal Node. Get a list of safe neighbour Nodes and increment G cost.
                var adjacentNodes = GetWalkableAdjacentSquares(current.X, current.Y);
                g++;

                //For each neighbour Node of the current Node.
                foreach (var adjacentNode in adjacentNodes)
                {
                    //If this adjacent Node is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.X == adjacentNode.X
                            && l.Y == adjacentNode.Y) != null)
                        continue;

                    //If it is not in the open list
                    if (openList.FirstOrDefault(l => l.X == adjacentNode.X
                            && l.Y == adjacentNode.Y) == null)
                    {
                        //Update the Node's G, H and F score, and set the parent.
                        adjacentNode.G = g;
                        adjacentNode.H = ComputeHScore(adjacentNode.X,adjacentNode.Y, goal_x, goal_y);
                        adjacentNode.F = adjacentNode.G + adjacentNode.H;
                        adjacentNode.Parent = current;

                        //Add the neighbour to the open list
                        openList.Insert(0, adjacentNode);
                    }

                    //Otherwise the neighbour Node is already in the open list.
                    else
                    {
                        //Check if using the current G score lowers the Node's F score.
                        //If it will be lower, update the parent becuase that means we have found a better path to this Node.
                        if (g + adjacentNode.H < adjacentNode.F)
                        {
                            adjacentNode.G = g;
                            adjacentNode.F = adjacentNode.G + adjacentNode.H;
                            adjacentNode.Parent = current;
                        }
                    }
                }
            }

            //If the openList is empty, then there must be no safe path to the objective.
            if (openList.Count == 0)
            {             
                Console.WriteLine("There is no safe path to the objective.");
            }
        }

        /// <summary>
        /// This method takes a list of coordinates and then prints the steps taken and change in direction which they represent
        /// </summary>
        /// <param name="Coordinates">A list of adjacent Nodes representing a path to travel</param>
        public static void PrintDirection(List<Node> Coordinates)
        {
            //Initalise the list to print.
            List<string> statements = new List<string>();

            //Initalise my step counter at 1.
            int counter = 1;

            //Initalise direction strings as null because I do not yet know which direction I will be travelling.
            string ?currentdirection = null;
            string ?newdirection = null;

            //Begin the loop by comparing the current node to the previous node.
            for (int i = 1; i < Coordinates.Count; i++)
            {
                //Calculate current direction by comparing the difference in X and Y value between this Node and the previous Node.
                int diffX = Coordinates[i].X - Coordinates[i - 1].X;
                int diffY = Coordinates[i].Y - Coordinates[i - 1].Y;

                //If the difference between X values is positive, I must be heading East
                if (diffX > 0)
                {
                    newdirection = "east";
                }

                //If the difference between X values is negative, I must be heading West
                if (diffX < 0)
                {
                    newdirection = "west";
                }

                //If the difference between Y values is positive, I must be heading North
                if (diffY > 0)
                {
                    newdirection = "north";
                }

                //If the difference between Y values is negative, I must be heading South
                else if (diffY < 0)
                {
                    newdirection = "south";
                }

                //If my direction does not change, add 1 step to the step counter.
                if (currentdirection == newdirection)
                {
                    counter++;
                }

                //If I do not yet have a current direction, set it to the direction I am travelling. 
                if (currentdirection == null)
                {
                    currentdirection = newdirection;
                }

                //If I am about to change direction, decide how many steps already taken
                //and add steps plus direction to the list of statements to print.
                if (currentdirection != newdirection)
                {
                    if (counter == 1)
                    {
                        statements.Add($"Head {currentdirection} for {counter} klick.");
                    }
                    else
                    {
                        statements.Add($"Head {currentdirection} for {counter} klicks.");
                    }

                    //Reset my current direction and reset step counter.
                    currentdirection = newdirection;
                    counter = 1;
                }
            }

            //For the last step in the list of coordinates.
            if (counter == 1)
            {
                statements.Add($"Head {newdirection} for {counter} klick.");
            }
            else
            {
                statements.Add($"Head {newdirection} for {counter} klicks");
            }

            //I have taken all steps, now print my list of direction satements.
            foreach (var statement in statements)
            {
                Console.WriteLine(statement);
            }
        }
    }
}
