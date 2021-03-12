using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeSpace
{
    public class Point
    {
        int x;
        int y;

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if(value < 0)
                {
                    x = Game.WIDTH - 1;
                }
                else if(value >= Game.WIDTH)
                {
                    x = 0;
                }
                else
                {
                    x = value;
                }
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if(value < 0)
                {
                    y = Game.HEIGHT - 1;
                }
                else if(value >= Game.HEIGHT)
                {
                    y = 0;
                }
                else
                {
                    y = value;
                }
            }
        }
    }
}