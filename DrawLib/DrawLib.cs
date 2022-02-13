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
		/// <summary>
		/// Sets up the given suface for drawing and returns a draw frame
		/// A: Sets the background color to SE blue
		/// B: switches the content to script
		/// C: unsets any previously selected script
		/// </summary>
		/// <param name="surface">The <see cref="Sandbox.ModAPI.Ingame.IMyTextSurface"/> to prepare for drawing</param>
		/// <returns>A frame for drawing on the given surface</returns>
		public static MySpriteDrawFrame PrepareDrawSurface(IMyTextSurface surface)
		{
			// Draw background color
			surface.ScriptBackgroundColor = new Color(0, 128, 255, 255);

			// Set content type
			surface.ContentType = ContentType.SCRIPT;

			// Set script to none
			surface.Script = "";

			return surface.DrawFrame();
		}
	}
}