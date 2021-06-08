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
        //Weird Blocks:
        //Rotors: Forward is in the Diretion of the 180° Marking, NOT the 0° marking, left is the 270° marking, up in up from the base
        //Hinges: Forward is in the direction of +90°, Left is up from the Base, Up is perpendicular to both
        //Programmable Blocks: Forward is through the back, ie the way one is facing when standing in front of the PB's display

        private static MatrixD GetGrid2WorldTransform(IMyCubeGrid grid)
        {
            Vector3D origin = grid.GridIntegerToWorld(new Vector3I(0, 0, 0));
            Vector3D plusY = grid.GridIntegerToWorld(new Vector3I(0, 1, 0)) - origin;
            Vector3D plusZ = grid.GridIntegerToWorld(new Vector3I(0, 0, 1)) - origin;
            return MatrixD.CreateScale(grid.GridSize) * MatrixD.CreateWorld(origin, -plusZ, plusY);
        }
        private static MatrixD GetBlock2WorldTransform(IMyCubeBlock blk)
        {
            Matrix blk2grid;
            blk.Orientation.GetMatrix(out blk2grid);
            return blk2grid *
                   MatrixD.CreateTranslation(((Vector3D)new Vector3D(blk.Min + blk.Max)) / 2.0) *
                   GetGrid2WorldTransform(blk.CubeGrid);
        }

        public static MatrixD AbsoluteBlockOrientation(IMyCubeBlock block)
        {
            return GetBlock2WorldTransform(block);
        }
        public static Vector3D AbsoluteBlockForward(IMyCubeBlock block)
        {
            MatrixD b2w = GetBlock2WorldTransform(block);
            Vector3D blockForward = b2w.Forward;
            blockForward.Normalize(); //(Need to normalize because the above matrices are scaled by grid size)
            return blockForward;
        }
        public static Vector3D AbsoluteBlockLeft(IMyCubeBlock block)
        {
            MatrixD b2w = GetBlock2WorldTransform(block);
            Vector3D blockLeft = b2w.Left;
            blockLeft.Normalize(); //(Need to normalize because the above matrices are scaled by grid size)
            return blockLeft;
        }
        public static Vector3D AbsoluteBlockUp(IMyCubeBlock block)
        {
            MatrixD b2w = GetBlock2WorldTransform(block);
            Vector3D blockUp = b2w.Up;
            blockUp.Normalize(); //(Need to normalize because the above matrices are scaled by grid size)
            return blockUp;
        }
        public static Vector3D AbsoluteBlockBackwards(IMyCubeBlock block)
        {
            MatrixD b2w = GetBlock2WorldTransform(block);
            Vector3D blockBack = b2w.Backward;
            blockBack.Normalize(); //(Need to normalize because the above matrices are scaled by grid size)
            return blockBack;
        }

        public static Vector3D AbsoluteBlockRight(IMyCubeBlock block)
        {
            MatrixD b2w = GetBlock2WorldTransform(block);
            Vector3D blockRight = b2w.Right;
            blockRight.Normalize(); //(Need to normalize because the above matrices are scaled by grid size)
            return blockRight;
        }

        public static Vector3D AbsoluteBlockDown(IMyCubeBlock block)
        {
            MatrixD b2w = GetBlock2WorldTransform(block);
            Vector3D blockDown = b2w.Down;
            blockDown.Normalize(); //(Need to normalize because the above matrices are scaled by grid size)
            return blockDown;
        }



        public static MatrixD AbsoluteGridOrientation(IMyCubeGrid grid)
        {
            return GetGrid2WorldTransform(grid);
        }
        public static Vector3D AbsoluteGridForward(IMyCubeGrid grid)
        {
            var grid2World = GetGrid2WorldTransform(grid);
            Vector3D gridForward = grid2World.Forward;
            gridForward.Normalize();//(Need to normalize because the above matrices are scaled by grid size)
            return gridForward;
        }
        public static Vector3D AbsoluteGridLeft(IMyCubeGrid grid)
        {
            var grid2World = GetGrid2WorldTransform(grid);
            Vector3D gridLeft = grid2World.Left;
            gridLeft.Normalize();//(Need to normalize because the above matrices are scaled by grid size)
            return gridLeft;
        }
        public static Vector3D AbsoluteGridUp(IMyCubeGrid grid)
        {
            var grid2World = GetGrid2WorldTransform(grid);
            Vector3D gridUp = grid2World.Up;
            gridUp.Normalize();//(Need to normalize because the above matrices are scaled by grid size)
            return gridUp;
        }
        public static Vector3D AbsoluteGridBackwards(IMyCubeGrid grid)
        {
            var grid2World = GetGrid2WorldTransform(grid);
            Vector3D gridBack = grid2World.Backward;
            gridBack.Normalize();//(Need to normalize because the above matrices are scaled by grid size)
            return gridBack;
        }
        public static Vector3D AbsoluteGridRight(IMyCubeGrid grid)
        {
            var grid2World = GetGrid2WorldTransform(grid);
            Vector3D gridRight = grid2World.Right;
            gridRight.Normalize();//(Need to normalize because the above matrices are scaled by grid size)
            return gridRight;
        }
        public static Vector3D AbsoluteGridDown(IMyCubeGrid grid)
        {
            var grid2World = GetGrid2WorldTransform(grid);
            Vector3D gridDown = grid2World.Down;
            gridDown.Normalize();//(Need to normalize because the above matrices are scaled by grid size)
            return gridDown;
        }
        public static void PrintBlockOrientation(IMyCubeBlock block, MyGridProgram parent)
        {
            parent.Echo("Forward: " + FormatVector(AbsoluteBlockForward(block)));
            parent.Echo("Up: " + FormatVector(AbsoluteBlockUp(block)));
            parent.Echo("Left: " + FormatVector(AbsoluteBlockLeft(block)));
            parent.Echo("Backwards: " + FormatVector(AbsoluteBlockBackwards(block)));
            parent.Echo("Down: " + FormatVector(AbsoluteBlockDown(block)));
            parent.Echo("Right: " + FormatVector(AbsoluteBlockRight(block)));
        }
    }
}
