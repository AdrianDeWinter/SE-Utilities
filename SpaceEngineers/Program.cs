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
        public partial class Implementation : ScriptBase
        {
            public Implementation(MyGridProgram parent, MyIni custom, MyIni storage, MyIniParseResult customSectionResult) : base(custom, storage, customSectionResult)
            {
                mainOverridden = false;
                saveOverridden = false;
                parent.Runtime.UpdateFrequency = UpdateFrequency.None;
            }
        }
    }
}
