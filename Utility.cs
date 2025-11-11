using CAB201_Object_Oriented_Design_and_Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CAB201_Object_Oriented_Design_and_Implementation
{

    /// <summary>
    /// The Utility class contains helper methods related to menu actions that the user can take
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// This method prints the user menu to the console
        /// </summary>
        static public void Help()
        {
            Console.WriteLine("");
            Console.WriteLine("Valid commands are:");
            Console.WriteLine("add guard <x> <y>: registers a guard obstacle");
            Console.WriteLine("add fence <x> <y> <orientation> <length>: registers a fence obstacle. " +
                "Orientation must be 'east' or 'north'.");
            Console.WriteLine("add sensor <x> <y> <radius>: registers a sensor obstacle");
            Console.WriteLine("add camera <x> <y> <direction>: registers a camera obstacle. " +
                "Direction must be 'north', 'south', 'east' or 'west'.");
            Console.WriteLine("check <x> <y>: checks whether a location and its surroundings are safe");
            Console.WriteLine("map <x> <y> <width> <height>: draws a text - based map of registered obstacles");
            Console.WriteLine("path <agent x> <agent y> <objective x> <objective y>: finds a path free of obstacles");
            Console.WriteLine("help: displays this help message");
            Console.WriteLine("exit: closes this program");
            Console.WriteLine("");
        }
    }
}
