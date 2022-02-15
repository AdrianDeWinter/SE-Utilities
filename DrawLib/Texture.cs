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
		/// A Texture Object is a set of <see cref="VRage.Game.GUI.TextPanel.MySprite"/>'s.
		/// It allows multiple sprites to scale, rotate and translate as one unit.
		/// </summary>
		public class Texture
		{
			/// <summary>
			/// Specifies wich colour slot a given sprite should bind to
			/// </summary>
			public enum ColorSlot
            {
				None,
				Background,
				Primary,
				Secondary,
				Tertiary
            }
			/// <summary>
			/// The private sprites member containes the sprites that have been added to the Texture, and their assigned colour slot
			/// </summary>
			private readonly List<MyTuple<MySprite, Texture.ColorSlot>> sprites;

			/// <summary>
			/// A publicly visible name, mostly useful for debug outputs. Default textures will have this set, it is, however, not required
			/// </summary>
			public string Name;

			/// <summary>
			/// The position at wich the texture should be displayed.
			/// This is applied as an offset to each individual sprite's location,
			/// and assumes the texture they form to be centered.
			/// </summary>
			public Vector2 Position;

			/// <summary>
			/// Texture sprites use this as a rotation in Radians, Text sprites ue it as text size.
			/// Unfortunately, Text sprites also ignore the Scale attribute, so this is the only way to scale them.
			/// 
			/// TODO: split this up for easier usability, and cover up this game limitation
			/// </summary>
			public float RotationOrScale;

			/// <summary>
			/// A Vector containing scaling factors in each axis. 1.0 means the Texture sprite will be displayed at its original size (at least in that axis)
			/// </summary>
			public Vector2 Scale;

			/// <summary>
			/// The actual width of the Texture in pixels (including scaling)
			/// </summary>
			public float Width { get { return width * Scale.X; } set { Scale.X = width / Width; } }
			private float width;

			/// <summary>
			/// The actual height of the Texture in pixels (including scaling)
			/// </summary>
			public float Height { get { return height * Scale.Y; } set { Scale.Y = height / Height; } }
			private float height;

			/// <summary>
			/// The colour that gets assigned to all sprites classed as "background"
			/// </summary>
			public Color Background = new Color(255, 255, 255, 255);

			/// <summary>
			/// The colour that gets assigned to all sprites classed as "primary"
			/// </summary>
			public Color Primary = new Color(255, 255, 255, 255);
			
			/// <summary>
			/// The colour that gets assigned to all sprites classed as "secondary"
			/// </summary>
			public Color Secondary = new Color(255, 255, 255, 255);
			
			/// <summary>
			/// The colour that gets assigned to all sprites classed as "tertiary"
			/// </summary>
			public Color Tertiary = new Color(255, 255, 255, 255);

			/// <summary>
			/// This constructor takes between zero and three arguments and initializes a new texture.
			/// </summary>
			/// <param name="_name">Sets the display name. If not specified, "texture" will be used as default</param>
			/// <param name="_sprites">A list of sprites, if nothing or null is passed, a new empty List is initialized instead</param>
			/// <param name="_rotation">A default Value for RotationOrScale. Keep in mind that Text sprites can not be Rotated, and will be scaled instead</param>
			public Texture(string _name = "texture", List<MyTuple<MySprite,Texture.ColorSlot>> _sprites = null, float _rotation = 0f, float _width = 0, float _height = 0, Dictionary<string,Color> _colors = null)
			{
				if (_sprites == null)
					sprites = new List<MyTuple<MySprite, Texture.ColorSlot>>();
				else
					sprites = _sprites;

				Name = _name;

				RotationOrScale = _rotation;

				Position = new Vector2(0f, 0f);

				Scale = new Vector2(1f, 1f);

				width = _width;

				height = _height;

				if(_colors != null)
                {
					if (_colors.ContainsKey("Background"))
						Background = _colors.GetValueOrDefault("Background");
					if (_colors.ContainsKey("Primary"))
						Primary = _colors.GetValueOrDefault("Primary");
					if (_colors.ContainsKey("Secondary"))
						Secondary = _colors.GetValueOrDefault("Secondary");
					if (_colors.ContainsKey("Tertiary"))
						Tertiary = _colors.GetValueOrDefault("Tertiary");
				}
			}

			/// <summary>
			/// This method adds all sprites in this texture to the given frame, applying the appropriate offsets and scaling factors to them.
			/// </summary>
			/// <param name="Frame">The frame to add the sprites to</param>
			public void AddToFrame(ref MySpriteDrawFrame Frame)
			{
				foreach (MyTuple<MySprite,Texture.ColorSlot> sp in sprites)
				{
					//copy the sprite
					MySprite sprite = sp.Item1;

					//apply position offsets according to texture position adn scale
					sprite.Position *= Scale;
					sprite.Position += Position;

					//scale it according to texture scale
					sprite.Size *= Scale;

					//rotate (or scale for text)
					sprite.RotationOrScale += RotationOrScale;

					//apply colours
                    switch (sp.Item2)
                    {
						case ColorSlot.None: break;
						case ColorSlot.Background: sprite.Color = Background; break;
						case ColorSlot.Primary: sprite.Color = Primary; break;
						case ColorSlot.Secondary: sprite.Color = Secondary; break;
						case ColorSlot.Tertiary: sprite.Color = Tertiary; break;
					}

					//add to the frame to render
					Frame.Add(sprite);
				}
			}

			/// <summary>
			/// Converts the Texture Object into a human readable form. It's a ToString(), what did you expect...
			/// </summary>
			/// <returns>Human readable string</returns>
			public override string ToString()
			{
				return "Name: " + Name
					+ "\nRotation/Scale: " + RotationOrScale
					+ "\nSprite count: " + sprites.Count
					+ "\nPosition: " + FormatVector(Position)
					+ "\nScale: " + FormatVector(Scale);
			}
		}
	}
}
