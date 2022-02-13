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
		/// This static class holds a number of defaukt textures, for both demonstration purposes and as useful building blocks.
		/// TODO: add the entire SE font as textures, so they can rotate and scale like normal sprites
		/// </summary>
		public static class DefaultTextures
		{
			/// <summary>
			/// A simple smiley. Stolen directly from Whiplash141's wiki for his excellent SpriteBuilder.
			/// Check him out: https://gitlab.com/whiplash141/spritebuilder
			/// </summary>
			public static Texture Smiley = new Texture("Smiley", new List<MySprite>{
						new MySprite()
						{
							Type = SpriteType.TEXTURE,
							Alignment = TextAlignment.CENTER,
							Data = "Circle",
							Position = new Vector2(0f, 0f),
							Size = new Vector2(100f, 100f),
							Color = new Color(255, 255, 0, 255),
							RotationOrScale = 0f
						}, // Face
						new MySprite()
						{
							Type = SpriteType.TEXTURE,
							Alignment = TextAlignment.CENTER,
							Data = "Circle",
							Position = new Vector2(-20f, -20f),
							Size = new Vector2(15f, 15f),
							Color = new Color(255, 255, 255, 255),
							RotationOrScale = 0f
						}, // Eye
						new MySprite()
						{
							Type = SpriteType.TEXTURE,
							Alignment = TextAlignment.CENTER,
							Data = "Circle",
							Position = new Vector2(20f, -20f),
							Size = new Vector2(15f, 15f),
							Color = new Color(255, 255, 255, 255),
							RotationOrScale = 0f
						}, // Eye
						new MySprite()
						{
							Type = SpriteType.TEXTURE,
							Alignment = TextAlignment.CENTER,
							Data = "SemiCircle",
							Position = new Vector2(0f, 0f),
							Size = new Vector2(80f, 80f),
							Color = new Color(255, 255, 255, 255),
							RotationOrScale = 3.1416f
						} // Mouth
					});
			public static Texture NoEntry = new Texture("No Entry", new List<MySprite>{
						new MySprite()
						{
							Type = SpriteType.TEXTURE,
							Alignment = TextAlignment.CENTER,
							Data = "No Entry",
							Position = new Vector2(0f, 0f),
							Size = new Vector2(100f, 100f),
							Color = new Color(255, 255, 0, 255),
							RotationOrScale = 0f
						}
					});
		}
	}
}
