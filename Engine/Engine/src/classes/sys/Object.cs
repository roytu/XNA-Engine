using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public abstract class Object
    {
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
                if (value != null)
                {
                    width = sprite.Width;
                    height = sprite.Height;
                }
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
        public float angle;
        public float depth;
        public Color color;

        public float frame;
        public int frameCount;
        public float frameSpeed;

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
            angle = 0;
            depth = 0;
            color = Color.White;

            frame = 0;
            frameCount = 1;
            frameSpeed = 0;
            bbox = new Rectangle();

            Game1.hObjCont.objectArray.Add(this);
        }
        ~Object()
        {
            Game1.hObjCont.objectArray.RemoveAll
                (
                delegate(Object _o)
                {
                    return _o == this;
                }
                );
        }
        public void Remove()
        {
            Game1.hObjCont.objectArray.RemoveAll
                (
                delegate(Object _o)
                {
                    return _o == this;
                }
                );
        }
        public virtual void Update()
        {
            frame += frameSpeed;
            if (frame >= frameCount)
            {
                frame -= frameCount;
            }
            if (Game1.currentMouseState.LeftButton == ButtonState.Pressed)
            {
                LeftClick(Game1.currentMouseState.X, Game1.currentMouseState.Y);
                if (Game1.prevMouseState.LeftButton == ButtonState.Released)
                {
                    LeftClicked(Game1.currentMouseState.X, Game1.currentMouseState.Y);
                }
            }
            if (Game1.currentMouseState.RightButton == ButtonState.Pressed)
            {
                RightClick(Game1.currentMouseState.X, Game1.currentMouseState.Y);
                if (Game1.prevMouseState.RightButton == ButtonState.Released)
                {
                    RightClicked(Game1.currentMouseState.X, Game1.currentMouseState.Y);
                }
            }
        }
        public virtual void LeftClick(int mouseX, int mouseY) { }
        public virtual void LeftClicked(int mouseX, int mouseY) { }
        public virtual void RightClick(int mouseX, int mouseY) { }
        public virtual void RightClicked(int mouseX, int mouseY) { }
        public virtual void Draw(SpriteBatch sb)
        {
            if (Sprite != null)
            {
                Rectangle destRect = new Rectangle((int)x, (int)y, (int)Math.Abs(Math.Round(width / frameCount * xscale)), (int)Math.Abs(Math.Round(height * yscale)));
                Rectangle srcRect = new Rectangle((int)frame * (width / frameCount), 0, width / frameCount, height);
                Vector2 orig = new Vector2(xoff, yoff);
                if (xscale < 0 || yscale < 0)
                {
                    if (xscale < 0)
                    {
                        sb.Draw(Sprite, destRect, srcRect, color, angle, orig, SpriteEffects.FlipHorizontally, depth);
                    }
                    else if (yscale < 0)
                    {
                        sb.Draw(Sprite, destRect, srcRect, color, angle, orig, SpriteEffects.FlipVertically, depth);
                    }
                }
                else
                {
                    sb.Draw(Sprite, destRect, srcRect, color, angle, orig, SpriteEffects.None, depth);
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