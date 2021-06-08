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
        public static class FindMethods
        {
            //for a lsit of blocks, return list of grids these blocks belong to (filtered to be unique)
            public static List<IMyCubeGrid> ParentGridsOfBlocks(List<IMyCubeBlock> blocks)
            {
                List<IMyCubeGrid> Grids = new List<IMyCubeGrid>();
                foreach (var block in blocks)
                    if (!Grids.Contains(block.CubeGrid))
                        Grids.Add(block.CubeGrid);
                return Grids;
            }

            //finds all IMySolarPanel in a list of IMyPowerProducer
            public static List<IMySolarPanel> FindSolarPanels(List<IMyPowerProducer> powerBlocks)
            {
                List<IMySolarPanel> solarPanels = new List<IMySolarPanel>();
                foreach (var block in powerBlocks)
                    if (block.GetType().Name == "MySolarPanel")
                        solarPanels.Add((IMySolarPanel)block);
                return solarPanels;
            }

            //finds all IMyBatteryBlock in a list of IMyPowerProducer
            public static List<IMyBatteryBlock> FindBatteries(List<IMyPowerProducer> powerBlocks)
            {
                List<IMyBatteryBlock> batteries = new List<IMyBatteryBlock>();
                foreach (var block in powerBlocks)
                    if (block.GetType().Name == "MyBatteryBlock")
                        batteries.Add((IMyBatteryBlock)block);
                return batteries;
            }


            //finds all IMyReactor in a list of IMyPowerProducer
            public static List<IMyReactor> FindReactors(List<IMyPowerProducer> powerBlocks)
            {
                List<IMyReactor> reactors = new List<IMyReactor>();
                foreach (var block in powerBlocks)
                    if (block.GetType().Name == "MyReactor")
                        reactors.Add((IMyReactor)block);
                return reactors;
            }

            //finds all Wind Turbines in a list of IMyPowerProducer
            public static List<IMyPowerProducer> FindWindTurbines(List<IMyPowerProducer> powerBlocks)
            {
                List<IMyPowerProducer> windTurbines = new List<IMyPowerProducer>();
                foreach (var block in powerBlocks)
                    if (block.GetType().Name == "MyWindTurbine")
                        windTurbines.Add((IMyPowerProducer)block);
                return windTurbines;
            }

            //finds all Hydrogen Engines in a list of IMyPowerProducer
            public static List<IMyPowerProducer> FindHydrogenEngines(List<IMyPowerProducer> powerBlocks)
            {
                List<IMyPowerProducer> hydrogenEngines = new List<IMyPowerProducer>();
                foreach (var block in powerBlocks)
                    if (block.GetType().Name == "MyHydrogenEngine")
                        hydrogenEngines.Add((IMyPowerProducer)block);
                return hydrogenEngines;
            }

            //finds all IMyPowerProducer that belong to the same Construct as refBlock
            public static List<IMyPowerProducer> FindPowerSources(IMyTerminalBlock refBlock, MyGridProgram program)
            {
                List<IMyPowerProducer> list = new List<IMyPowerProducer>();
                program.GridTerminalSystem.GetBlocksOfType<IMyPowerProducer>(list, block => block.IsSameConstructAs(refBlock));
                return list;
            }

            //finds all IMyShipConnector that belong to the same Construct as refBlock
            public static List<IMyShipConnector> FindConnectors(IMyTerminalBlock refBlock, MyGridProgram program)
            {
                List<IMyShipConnector> list = new List<IMyShipConnector>();
                program.GridTerminalSystem.GetBlocksOfType<IMyShipConnector>(list, block => block.IsSameConstructAs(refBlock));
                return list;
            }

            /// <summary>
            ///      adds up the current and max output of all elements in a list of IMyPowerProducer
            /// </summary>
            /// <param name="blocks">
            ///     The list of IMyPowerProducer to tally up
            /// </param>
            /// <param name="max">
            ///     ref to the float that will hold the sum of the maximum power outputs of the blocks in the list
            /// </param><param name="cur">
            ///     ref to the float that will hold the sum of the current power outputs of the blocks in the list
            /// </param>
            ///  <param name="batteryNetOnly">
            ///     if true, will test for battery blocks and subtract durrent battery ionput from output
            /// </param>
            public static void SumPowerCapacities(List<IMyPowerProducer> blocks, ref float max, ref float cur, bool batteryNetOnly = false)
            {
                max = 0;
                cur = 0;
                foreach(var block in blocks)
                {
                    if(block.Enabled)
                        max += block.MaxOutput;
                    if(batteryNetOnly && block.GetType().Name == "MyBatteryBlock")
                    {
                        IMyBatteryBlock battery = (IMyBatteryBlock)block;
                        cur += (battery.CurrentOutput - battery.CurrentInput);
                    }
                    cur += block.CurrentOutput;
                }
            }
        }
    }
}
