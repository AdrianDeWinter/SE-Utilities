using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        /*
         * Plans:
         * Default sprites:
         * contained in some sort of data structure or namespace
         * 
         * simplified drawing calls:
         * only block reference and surface id as parameters
         * optional: add knowledge of surface id's on blocks into script
         * 
         * Easily store sprite config (e.g. menus):
         * save/load sprite configs from text structure
         * easy function to draw on all blocks based on their config
         * 
         * Menus:
         * sets of sprites that auto display values
         * maybe via format strings and dict references    ?
         * 
         * 
         * 
         */
        
        public class DrawLib
        {

        }
    }
}
