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
        public static string StringifyPowerBlock(IMyPowerProducer block)
        {
            return block.CustomName + ", Producing: " + block.CurrentOutput;
        }

        public static string FormattedPower(double power)
        {
            int decimalPlaces = 1;

            if (power >= 1 || power <= -1)
                return (Math.Round(power, decimalPlaces) + "MW");
            else if (power >= 0.001 || power <= -0.001)
                return (Math.Round(power * 1000, decimalPlaces) + "kW");
            else if (power >= 0.000001 || power <= -0.000001)
                return (Math.Round(power * 1000000, decimalPlaces) + "W");
            else
                return (Math.Round(power * 1000000000, decimalPlaces) + "mW");
        }

        public static string formatAngle(double angle, bool inRadians = false)
        {
            int decimalPlaces = 1;

            //normalize to 0 <= angle <= 2*Pi
            if (angle >= 2 * Math.PI)
                angle -= 2 * Math.PI;

            if (inRadians)
                return angle + "rad";
            return Math.Round(angle/Math.PI*180, decimalPlaces) + "°";
        }

        public static string FormatVector(Vector3D vector)
        {
            int decimalPlaces = 1;

            return "X:" + Math.Round(vector.X, decimalPlaces) + " Y:" + Math.Round(vector.Y, decimalPlaces) + " Z:" + Math.Round(vector.Z, decimalPlaces);
        }
    }
}
