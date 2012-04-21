using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class ObjCont
    {
        public List<Object> objectArray;
        public ObjCont()
        {
            objectArray = new List<Object>();
        }
        public void Init()
        {
        }
        public void Update()
        {
            for (int i = 0; i < objectArray.Count; i++)
            {
                Object o = objectArray[i];
                o.Update();
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            for(int i=0;i<objectArray.Count;i++)
            {
                objectArray[i].Draw(sb);
            }
            sb.End();
        }
    }
}