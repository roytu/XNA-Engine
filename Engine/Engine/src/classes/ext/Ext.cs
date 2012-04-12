using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Engine
{
    class Ext
    {
        public static Color HSVtoRGB(int h, int s, int v)
        {
            double ch = h * 2 * Math.PI / 255;
            float cs = s / 255;
            float cv = v;
            int r = (int)(((Math.Cos(ch + 0 * Math.PI / 3) / 2 + 0.5) * (1 - cs)) * cv);
            int g = (int)(((Math.Cos(ch + 3 * Math.PI / 3) / 2 + 0.5) * (1 - cs)) * cv);
            int b = (int)(((Math.Cos(ch + 6 * Math.PI / 3) / 2 + 0.5) * (1 - cs)) * cv);
            return new Color(r, g, b);
        }
    }
}
