using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CAB201_Object_Oriented_Design_and_Implementation
{
    //References
    //Resources used to help understand A* pathfinding:
    //https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
    //https://robotics.caltech.edu/wiki/images/e/e0/Astar.pdf
    //https://www.youtube.com/watch?v=i0x5fj4PqP4

    /// <summary>
    /// Node represents a pair of x and y coordinates which is used by the Pathfinding class to navigate the map.
    /// Each node can be thought of as representing a single tile on the map for pathfinding purposes. 
    /// </summary>
    public class Node : Path
    {
        //Non-const fields are always private.

        /// <summary>
        /// The X coordinate of the node.
        /// </summary>
        private int x;

        /// <summary>
        /// The Y coordinate of the node.
        /// </summary>
        private int y;

        /// <summary>
        /// G represents the distance travelled from the starting node to this node
        /// </summary>
        private int g;

        /// <summary>
        /// H (Heuristic) represents the estimated distance from this node to the end (goal) node
        /// </summary>
        private int h;

        /// <summary>
        /// F is the sum of G + H. F is used by the pathfinder to estimate
        /// what is the most likely to be the shortest distance to the goal node.
        /// </summary>
        private int f;

        /// <summary>
        /// The parent is the node which preceded this node in the current path.
        /// It represents the node which is one step backwards on the path.
        /// This can be nullable because the very first node will not have a parent.
        /// </summary>
        private Node ?parent;


        //external access to private non-const fields is controlled through public getter and setter

        /// <summary>
        /// The X coordinate of the node.
        /// </summary>
        public int X { get { return x; } set { x = value; } }

        /// <summary>
        /// The Y coordinate of the node.
        /// </summary>
        public int Y { get { return y; } set { y = value; } }

        /// <summary>
        /// G represents the distance travelled from the starting node to this node
        /// </summary>
        public int G { get { return g; } set { g = value; } }

        /// <summary>
        /// H (Heuristic) represents the estimated distance from this node to the end (goal) node
        /// </summary>
        public int H { get { return h; } set { h = value; } }

        /// <summary>
        /// F is the sum of G + H. F is used by the pathfinder to estimate
        /// what is the most likely to be the shortest distance to the goal node.
        /// </summary>
        public int F { get { return f; } set { f = value; } }

        /// <summary>
        /// The parent is the node which preceded this node in the current path.
        /// It represents the node which is one step backwards on the path.
        /// It can be nullable because the very first Node will not have a parent.
        /// </summary>
        public Node ?Parent { get { return parent; } set { parent = value; } }


        /// <summary>
        /// Creates a new Node object with the given x and y coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public Node(int x, int y)
        {
            X = x;

            Y = y;

            //Set the distance from this node to the starting node(Manhattan distance)
            G = Math.Abs(Start_X - x) + Math.Abs(Start_Y - y);

            // Set the estimated distance from this node to the goal node (Manhattan distance)
            H = Math.Abs(Goal_X - x) + Math.Abs(Goal_Y - y);
        }
    }
}
