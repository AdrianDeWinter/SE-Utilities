using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        /// <summary>
        ///      attempts to parse a given string as SE's GPS Format
        /// </summary>
        /// <param name="gpsString">
        ///     the String to parse
        /// </param>
        /// <param name="vector">
        ///     ref to the Vector3D that will hold the result
        /// </param>
        /// <param name="parent">
        ///     optional parameter, if set, the function will print all substrings to the PBs text field
        /// </param>
        /// <returns>
        ///     true if the string was parsed successfully, false otherwise
        /// </returns>
        public static bool TryParseGPS(string gpsString, out Vector3D vector, MyGridProgram parent = null)
        {
            vector = new Vector3D(0, 0, 0);

            var gpsStringSplit = gpsString.Split(':');

            double x, y, z;

            if (parent != null)
                foreach (var i in gpsStringSplit)
                    parent.Echo(i);

            if (gpsStringSplit.Length != 7)
                return false;

            bool passX = double.TryParse(gpsStringSplit[2], out x);
            bool passY = double.TryParse(gpsStringSplit[3], out y);
            bool passZ = double.TryParse(gpsStringSplit[4], out z);

            if (passX && passY && passZ)
            {
                vector = new Vector3D(x, y, z);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        ///      attempts to parse a given string as a component vector, supports both ',' and '.' as decimal signs
        /// </summary>
        /// <remarks>
        ///      Supported Formats (x,y,z represent the float values):
        ///      x:y:z
        ///      
        ///      X:x,Y:y,Z:z
        ///      
        ///      {x:y:z}
        ///      
        ///      {X:x,Y:y,Z:z}
        ///      
        /// </remarks>
        /// <param name="gpsString">
        ///     the String to parse
        /// </param>
        /// <param name="vector">
        ///     ref to the Vector3D that will hold the result
        /// </param>
        /// <param name="parent">
        ///     optional parameter, if set, the function will print all substrings to the PBs text field
        /// </param>
        /// <returns>
        ///     true if the string was parsed successfully, false otherwise
        /// </returns>
        public static bool TryParseVector3D(string vectorString, out Vector3D vector, MyGridProgram parent = null)
        {
            vector = new Vector3D(0, 0, 0);

            vectorString = vectorString.Replace(" ", "").Replace("{", "").Replace("}", "").Replace("X", "").Replace("Y", "").Replace("Z", "").Replace("x", "").Replace("y", "").Replace("z", "").Replace(",",".");
            var vectorStringSplit = vectorString.Split(':');

            if (parent != null)
                foreach (var i in vectorStringSplit)
                    parent.Echo(i);

            double x, y, z;

            if (vectorStringSplit.Length < 3)
                return false;

            bool passX = double.TryParse(vectorStringSplit[0], out x);
            bool passY = double.TryParse(vectorStringSplit[1], out y);
            bool passZ = double.TryParse(vectorStringSplit[2], out z);

            if (passX && passY && passZ)
            {
                vector = new Vector3D(x, y, z);
                return true;
            }
            else
                return false;
        }
    }
}
