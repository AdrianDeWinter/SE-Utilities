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
        public static Vector3D PathOnPlane(PlaneD plane, Vector3D target, Vector3D startPoint)
        {
            return Vector3D.ProjectOnPlane(ref target, ref plane.Normal) - Vector3D.ProjectOnPlane(ref startPoint, ref plane.Normal);
        }
        public static Vector3D PathOnPlane(Vector3D normal, Vector3D target, Vector3D startPoint)
        {
            return Vector3D.ProjectOnPlane(ref target, ref normal) - Vector3D.ProjectOnPlane(ref startPoint, ref normal);
        }

        public static double AngleBetweenVectors(Vector3D vec1, Vector3D vec2)
        {
            double dotProduct = Vector3D.Dot(vec1, vec2);
            double magnitudeProduct = vec1.Length() * vec2.Length();
            double angle = Math.Acos(dotProduct / magnitudeProduct);
            //normalize to 0 <= angle <= 2*Pi
            if (angle >= 2 * Math.PI)
                angle -= 2 * Math.PI;
            
            return angle;
        }

        public static double Rad2deg(double angleInRadians)
        {
            return angleInRadians / Math.PI * 180;
        }

        public static double Deg2rad(double angleInDegrees)
        {
            return angleInDegrees / 180* Math.PI;
        }

        public static double PositiveAngle(double angle)
        {
            if (angle < 0)
                angle += 2 * Math.PI;
            return angle;
        }
    }
}
