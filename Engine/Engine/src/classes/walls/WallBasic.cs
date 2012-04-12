using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class WallBasic : Walls
    {
        public WallBasic(int x, int y) : base(x, y)
        {
            Sprite = Game1.texWall;
        }
        public override void Init()
        {
            base.Init();
            SetBbox(0, 0, width, height);
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
