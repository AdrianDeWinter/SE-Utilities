using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;
using Sandbox.Game.GameSystems;
using System.Xml;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        class Implementation : ScriptBase
        {
            private int counter = 0;
            private static readonly int versionInfoDisplayTime = 2;

            public Implementation(MyGridProgram parent, MyIni custom, MyIni storage, MyIniParseResult customSectionResult) : base(custom, storage, customSectionResult)
            {
                parent.Runtime.UpdateFrequency |= UpdateFrequency.Update100;
                parent.Runtime.UpdateFrequency |= UpdateFrequency.Update10;
                parent.Runtime.UpdateFrequency |= UpdateFrequency.Update1;
            }

            public override void OnTerminal(MyGridProgram parent, string arguments)
            {
                parent.Runtime.UpdateFrequency ^= UpdateFrequency.Update100;
                parent.Runtime.UpdateFrequency ^= UpdateFrequency.Update10;
                parent.Runtime.UpdateFrequency ^= UpdateFrequency.Update1;
                parent.Runtime.UpdateFrequency |= UpdateFrequency.Once;//use CallOnce to ensure no other updates that were triggered in the same tick as OnTermial overwrite version info
            }

            public override void OnCallOnce(MyGridProgram parent)
            {
                parent.Runtime.UpdateFrequency |= UpdateFrequency.Update100;
                counter = versionInfoDisplayTime;
                parent.Echo("Test Script Running");
                MyIni ini = new MyIni();
                bool res = ini.TryParse(parent.Me.CustomData,"AntennaSteer");
                parent.Echo(ini.Get("AntennaSteer", "blub").ToInt16().ToString());
            }

            public override void OnUpdate100(MyGridProgram parent)
            {
                if (counter != 0)
                    counter--;
                else
                {
                    parent.Runtime.UpdateFrequency |= UpdateFrequency.Update10;
                    parent.Runtime.UpdateFrequency |= UpdateFrequency.Update1;
                }
            }

            public override void OnUpdate10(MyGridProgram parent)
            {
            }

            public override void OnUpdate1(MyGridProgram parent)
            {
            }

        }
    }
}
