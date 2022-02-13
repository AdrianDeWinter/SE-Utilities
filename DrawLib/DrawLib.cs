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
	public class Texture
	{
		private List<MySprite> sprites;

		public string Name;

		public Vector2 Position;//position of the texture, applied as an offset to all sprites in the texture

		public float RotationOrScale;//rotation in radian

		public Vector2 Scale;//scaling factor in the x and y axes, 1.0 means origianl scale

		public Texture(string _name = "texture", List<MySprite> _sprites = null, float _rotation = 0f)
		{
			if (_sprites == null)
				sprites = new List<MySprite>();
			else
				sprites = _sprites;

			Name = _name;

			RotationOrScale = _rotation;

            Position = new Vector2(0f, 0f);

            Scale = new Vector2(1f, 1f);
		}

		public void AddToFrame(ref MySpriteDrawFrame Frame)
		{
			foreach (MySprite sp in sprites)
			{
				MySprite sprite = sp;

				sprite.Position *= Scale;
				sprite.Position += Position;

				sprite.Size *= Scale;

				sprite.RotationOrScale = RotationOrScale;

				Frame.Add(sprite);
			}
		}

		public override string ToString()
		{
			return "Name: " + Name
				+ "\nRotation/Scale: " + RotationOrScale
				+ "\nSprite count: " + sprites.Count
				+ "\nPosition: " + FormatVector(Position)
				+ "\nScale: " + FormatVector(Scale);
		}
	}

	public static struct DefaultTextures
    {
		public Texture Smiley = new Texture("Smiley", new List<MySprite>{
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
                })
    }
}