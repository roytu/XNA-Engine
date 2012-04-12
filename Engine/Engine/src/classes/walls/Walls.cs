using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class Walls : Object
    {
        public Walls(int x, int y) : base(x, y)
        {
            PreInit();
        }
        public virtual void PreInit() { }
        public virtual void Init()
        {
            SetBbox(0, 0, width, height);
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
