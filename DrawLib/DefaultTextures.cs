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
		/// This static class holds a number of default textures, for both demonstration purposes and as useful building blocks.
		/// TODO: add the entire SE font as textures, so they can rotate and scale like normal sprites
		/// </summary>
		public static class DefaultTextures
		{
			/// <summary>
			/// A simple smiley. Stolen directly from Whiplash141's wiki for his excellent SpriteBuilder.
			/// Check him out: https://gitlab.com/whiplash141/spritebuilder
			/// </summary>
			public static Texture Smiley = new Texture(_name : "Smiley", _width : 100, _height : 100, _sprites: new List<MySprite>{
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
						}, // Left Eye
						new MySprite()
						{
							Type = SpriteType.TEXTURE,
							Alignment = TextAlignment.CENTER,
							Data = "Circle",
							Position = new Vector2(20f, -20f),
							Size = new Vector2(15f, 15f),
							Color = new Color(255, 255, 255, 255),
							RotationOrScale = 0f
						}, // Right Eye
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
			/// <summary>
			/// Letter 'A' in SE's font
			/// </summary>
			public static Texture A = new Texture(_name: "A", _width: 260, _height: 185, _sprites: new List<MySprite>{
				new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(98f,0f), new Vector2(35f,150f), new Color(0,0,0,255), null, TextAlignment.CENTER, -0.192f), // Right Leg
				new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-98f,0f), new Vector2(35f,150f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0.192f), // Left Leg
				new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(0f,-75f), new Vector2(160f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Top Bar
				new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(0f,-5f), new Vector2(190f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Middle Bar
				new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(113f,75f), new Vector2(35f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Bottom Right Corner
				new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-113f,75f), new Vector2(35f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Bottom Left Corner
				new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-83f,-75f), new Vector2(35f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Top Left Corner
				new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(83f,-75f), new Vector2(35f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f) // Top Right Corner
			});
			/// <summary>
			/// Letter 'B' in SE's font
			/// </summary>
			public static Texture B = new Texture(_name: "B", _width: 260, _height: 185, _sprites: new List<MySprite>{
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-113f,0f), new Vector2(35f,150f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Left Bar
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-21f,75f), new Vector2(185f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Bottom Bar
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-21f,0f), new Vector2(185f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Middle Bar
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-21f,-75f), new Vector2(185f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Top Bar
						new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-113f,75f), new Vector2(35f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Corner Bottom Left
						new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-113f,-75f), new Vector2(35f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Corner Top Left
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(71f,35f), new Vector2(115f,115f), new Color(0,0,0,255), null, TextAlignment.CENTER, 1.5708f), // Bottom Circle
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(71f,-35f), new Vector2(115f,115f), new Color(0,0,0,255), null, TextAlignment.CENTER, 1.5708f), // Top Circle
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(71f,37f), new Vector2(40f,40f), new Color(255,255,255,255), null, TextAlignment.CENTER, 1.5708f), // Bottom Circle Cutout
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(71f,-37f), new Vector2(40f,40f), new Color(255,255,255,255), null, TextAlignment.CENTER, 1.5708f) // Top Circle Cutout
			});
		}
	}
}
