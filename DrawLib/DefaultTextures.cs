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
			public static Texture Smiley = new Texture(_name : "Smiley", _width : 100, _height : 100, _sprites: new List<MyTuple<MySprite, Texture.ColorSlot>>{
						new MyTuple<MySprite, Texture.ColorSlot>(
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
							Texture.ColorSlot.Primary),
						new MyTuple<MySprite, Texture.ColorSlot>(
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
							Texture.ColorSlot.Secondary),
						new MyTuple<MySprite, Texture.ColorSlot>(
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
							Texture.ColorSlot.Secondary),
						new MyTuple<MySprite, Texture.ColorSlot>(
							new MySprite()
							{
								Type = SpriteType.TEXTURE,
								Alignment = TextAlignment.CENTER,
								Data = "SemiCircle",
								Position = new Vector2(0f, 0f),
								Size = new Vector2(80f, 80f),
								Color = new Color(255, 255, 255, 255),
								RotationOrScale = 3.1416f
							}, // Mouth
							Texture.ColorSlot.Tertiary),
					},
				_colors : new Dictionary<string, Color> { { "Primary" , new Color(255, 255, 0, 255) }, { "Secondary", new Color(255, 255, 255, 255) } , { "Tertiary", new Color(255, 255, 255, 255) } });
			/// <summary>
			/// Letter 'A' in SE's font
			/// </summary>
			public static Texture A = new Texture(_name: "A", _width: 55, _height: 92.5f, _sprites: new List<MyTuple<MySprite, Texture.ColorSlot>>{
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(22.5f,15f), new Vector2(7f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Bottom Right Corner
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-22.5f,15f), new Vector2(7f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Bottom Left Corner
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-16.5f,-15f), new Vector2(7f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Top Left Corner
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(16.5f,-15f), new Vector2(7f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Top Right Corner
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(19.5f,0f), new Vector2(7f,30f), new Color(0,0,0,255), null, TextAlignment.CENTER, -0.192f),Texture.ColorSlot.Primary), // Right Leg
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-19.5f,0f), new Vector2(7f,30f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0.192f),Texture.ColorSlot.Primary), // Left Leg
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(0f,-15f), new Vector2(32f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Top Bar
				new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(0f,-1f), new Vector2(38f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Middle Bar
			},
			_colors: new Dictionary<string, Color> { { "Primary", new Color(0, 0, 0, 255) } });

			/// <summary>
			/// Letter 'B' in SE's font
			/// </summary>
			public static Texture B = new Texture(_name: "B", _width: 55, _height: 92.5f, _sprites: new List<MyTuple<MySprite, Texture.ColorSlot>>{
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-22.5f,15f), new Vector2(7f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Corner Bottom Left
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "Circle", new Vector2(-22.5f,-15f), new Vector2(7f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Corner Top Left
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-22.5f,0f), new Vector2(7f,30f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Left Bar
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-4f,15f), new Vector2(37f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Bottom Bar
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-4f,0f), new Vector2(37f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Middle Bar
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(-4f,-15f), new Vector2(37f,7f), new Color(0,0,0,255), null, TextAlignment.CENTER, 0f),Texture.ColorSlot.Primary), // Top Bar
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(10f,7f), new Vector2(23f,23f), new Color(0,0,0,255), null, TextAlignment.CENTER, 1.5708f),Texture.ColorSlot.Primary), // Bottom Circle
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(10f,-7f), new Vector2(23f,23f), new Color(0,0,0,255), null, TextAlignment.CENTER, 1.5708f),Texture.ColorSlot.Primary), // Top Circle
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(9f,7.5f), new Vector2(8f,8f), new Color(255,255,255,255), null, TextAlignment.CENTER, 1.5708f),Texture.ColorSlot.Background), // Bottom Circle Cutout
						new MyTuple<MySprite, Texture.ColorSlot>(new MySprite(SpriteType.TEXTURE, "SemiCircle", new Vector2(9f,-7.5f), new Vector2(8f,8f), new Color(255,255,255,255), null, TextAlignment.CENTER, 1.5708f),Texture.ColorSlot.Background) // Top Circle Cutout
			},
			_colors: new Dictionary<string, Color> { { "Primary", new Color(0, 0, 0, 255) } });
		}
	}
}
