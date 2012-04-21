using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    class ExtClasses
    {
        public class Grid<T>
        {
            private List<List<T>> grid;
            public int width;
            public int height;

            public Grid(int width, int height, T def)
            {
                grid = new List<List<T>>();

                this.width = width;
                this.height = height;
                for (uint x = 0; x < width; x++)
                {
                    List<T> ls = new List<T>();
                    for (uint y = 0; y < height; y++)
                    {
                        ls.Add(def);
                    }
                    grid.Add(ls);
                }
            }

            public T Get(int x, int y)
            {
                return grid[x][y];
            }
            public void Set(int x, int y, T value)
            {
                grid[x][y] = value;
            }
        }
    }
}