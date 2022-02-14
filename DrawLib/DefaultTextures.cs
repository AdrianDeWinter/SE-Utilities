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
			public static Texture A = new Texture("A", new List<MySprite>{
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "SquareSimple",
						Position = new Vector2(100f, 0f),
						Size = new Vector2(30f, 200f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = -0.192f
					}, // Right Leg
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "SquareSimple",
						Position = new Vector2(-100f, 0f),
						Size = new Vector2(30f, 200f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = 0.192f
					}, // Left Leg
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "SquareSimple",
						Position = new Vector2(0f, -100f),
						Size = new Vector2(140f, 30f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = 0f
					}, // Top Bar
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "SquareSimple",
						Position = new Vector2(0f, -5f),
						Size = new Vector2(200f, 30f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = 0f
					}, // Middle Bar
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "Circle",
						Position = new Vector2(119f, 98f),
						Size = new Vector2(30f, 30f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = 0f
					}, // Bottom Right Corner
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "Circle",
						Position = new Vector2(-119f, 98f),
						Size = new Vector2(30f, 30f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = 0f
					}, // Bottom Left Corner
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "SemiCircle",
						Position = new Vector2(-75f, -93f),
						Size = new Vector2(45f, 45f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = -0.384f
					}, // Top Left Corner
					new MySprite()
					{
						Type = SpriteType.TEXTURE,
						Alignment = TextAlignment.CENTER,
						Data = "SemiCircle",
						Position = new Vector2(74f, -93f),
						Size = new Vector2(45f, 45f),
						Color = new Color(0, 0, 0, 255),
						RotationOrScale = 0.384f
					} // Top Right Corner
			});
			/// <summary>
			/// Letter 'B' in SE's font
			/// </summary>
			public static Texture B = new Texture("B", new List<MySprite>{
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-95f,0f), new Vector2(35f,170f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Left Bar
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(0f,75f), new Vector2(200f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Bottom Bar
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(0f,0f), new Vector2(200f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Middle Bar
						new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(0f,-75f), new Vector2(200f,35f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Top Bar
						new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-102f,82f), new Vector2(20f,20f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Corner Bottom Left
						new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-102f,-82f), new Vector2(20f,20f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f), // Corner Top Left
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(99f,37f), new Vector2(110f,90f), new Color(0,0,0,255), null, TextAlignment.CENTER, 1.5708f), // Bottom Circle
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(99f,-37f), new Vector2(110f,90f), new Color(0,0,0,255), null, TextAlignment.CENTER, 1.5708f), // Top Circle
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(99f,37f), new Vector2(40f,30f), new Color(255,255,255,255), null, TextAlignment.CENTER, 1.5708f), // Bottom Circle Cutout
						new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(99f,-37f), new Vector2(40f,30f), new Color(255,255,255,255), null, TextAlignment.CENTER, 1.5708f) // Top Circle Cutout

			});
		}
	}
}
