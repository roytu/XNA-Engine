using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class Room
    {
        public List<String> data;
        public int roomWidth = 0;
        public int roomHeight = 0;
        public Room()
        {
            data = new List<String>();
        }
    }
}
