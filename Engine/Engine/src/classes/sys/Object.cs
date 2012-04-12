using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public abstract class Object
    {
        protected int LAYER_H = 0;
        protected int LAYER_V = 0;

        public bool isRotating = false;
        public int layer;
        public int layerHV;

        private Texture2D sprite;
        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
                width = sprite.Width;
                height = sprite.Height;
            }
        }
        public float x;
        public float y;
        public int xoff;
        public int yoff;
        public int width;
        public int height;
        public float xscale;
        public float yscale;
        public Rectangle bbox;

        public Object(int x=0, int y=0)
        {
            this.x = x;
            this.y = y;
            xoff = 0;
            yoff = 0;
            width = 0;
            height = 0;
            sprite = null;
            xscale = 1;
            yscale = 1;
            bbox = new Rectangle();

            Game1.hObjCont.objectArray.Add(this);
        }
        ~Object()
        {
            int _i = Game1.hObjCont.objectArray.RemoveAll
                (
                delegate(Object _o)
                {
                    return _o == this;
                }
                );
        }
        public virtual void Update(){}
        public virtual void Draw(SpriteBatch sb)
        {
            if (Sprite != null)
            {
                if (xscale < 0 || yscale < 0)
                {
                    if (xscale < 0)
                    {
                        sb.Draw(Sprite, new Microsoft.Xna.Framework.Rectangle((int)x, (int)y, (int)Math.Abs(Math.Round(width * xscale)), (int)Math.Abs(Math.Round(height * yscale))), null, Color.White, 0, new Vector2(xoff, yoff), SpriteEffects.FlipHorizontally, 0);
                    }
                    else if (yscale < 0)
                    {
                        sb.Draw(Sprite, new Microsoft.Xna.Framework.Rectangle((int)x, (int)y, (int)Math.Abs(Math.Round(width * xscale)), (int)Math.Abs(Math.Round(height * yscale))), null, Color.White, 0, new Vector2(xoff, yoff), SpriteEffects.FlipVertically, 0);
                    }
                }
                else
                {
                    sb.Draw(Sprite, new Microsoft.Xna.Framework.Rectangle((int)x, (int)Math.Round(y), (int)Math.Abs(Math.Round(width * xscale)), (int)Math.Abs(Math.Round(height * yscale))), null, Color.White, 0, new Vector2(xoff, yoff), SpriteEffects.None, 0);
                }
            }
        }
        protected void SetBbox(int x, int y, int w, int h)
        {
            bbox.X = x;
            bbox.Y = y;
            bbox.Width = w;
            bbox.Height = h;
        }
        protected bool isCollidingAt(double x, double y, Type obj)
        {
            for (int i = 0; i < Game1.hObjCont.objectArray.Count; i++)
            {
                GameHandler gh = Game1.hRoomCont.gameHandler;
                Object _o = Game1.hObjCont.objectArray[i];
                if (_o.GetType().IsSubclassOf(obj))
                {
                    double x11 = x + bbox.X;
                    double y11 = y + bbox.Y;
                    double x12 = x + bbox.X + bbox.Width;
                    double y12 = y + bbox.Y + bbox.Height;
                    double x21 = _o.x + _o.bbox.X;
                    double y21 = _o.y + _o.bbox.Y;
                    double x22 = _o.x + _o.bbox.X + _o.bbox.Width;
                    double y22 = _o.y + _o.bbox.Y + _o.bbox.Height;
                    if (((x21 <= x11 && x11 <= x22) || (x21 <= x12 && x12 <= x22)) && ((y21 <= y11 && y11 <= y22) || (y21 <= y12 && y12 <= y22)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        protected bool isCollidingAt(double x, double y, Predicate<Object> del)
        {
            for (int i = 0; i < Game1.hObjCont.objectArray.Count; i++)
            {
                Object _o = Game1.hObjCont.objectArray[i];
                if (del.Invoke(_o))
                {
                    double x11 = x + bbox.X;
                    double y11 = y + bbox.Y;
                    double x12 = x + bbox.X + bbox.Width;
                    double y12 = y + bbox.Y + bbox.Height;
                    double x21 = _o.x + _o.bbox.X;
                    double y21 = _o.y + _o.bbox.Y;
                    double x22 = _o.x + _o.bbox.X + _o.bbox.Width;
                    double y22 = _o.y + _o.bbox.Y + _o.bbox.Height;
                    if (((x21 < x11 && x11 < x22) || (x21 < x12 && x12 < x22)) && ((y21 < y11 && y11 < y22) || (y21 < y12 && y12 < y22)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}