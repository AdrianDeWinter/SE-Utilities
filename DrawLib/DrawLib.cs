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
	class texture
	{
		private List<MySprite> sprites;

		public string Name;

		public Vector2 Position;//position of the texture, applied as an offset to all sprites in the texture

		public float RotationOrScale;//rotaion in radian

		public Vector2 Scale;//scaleing factor in the x and y axes, 1.0 means origianl scale

		texture(string _name = "texture", List<MySprite> _sprites = null, _rotation = 0f, _position = new Vector2(0,0), _scale = new Vector2(1,1))
		{
			if _sprites == null
				sprites = new List<MySprite>();
			else
				sprites = _sprites;

			Name = _name;

			Position = _position;

			Rotation = _rotation;

			Scale = _scale;	
		}

		void addToFrame(MySpriteDrawFrame frame)
		{
			foreach (MySprite sp in sprites){
				MySprite sprite = sp;

				sprite.Position = sprite.Position * 
            }
		}
	} 
}
