using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeSpace
{
    public class Food : GameObject
    {
        Point location;
        Random random;

        public Food() { }

        public Food(char sign, ConsoleColor color) : base(sign, color)
        {
            random = new Random();
            location = new Point { X = 25, Y = 25 };
            body.Add(location);
            Draw();
        }

        public void Generate(Snake snake, Wall wall)
        {
            lock(locker)
            {
                while(true)
                {
                    start:
                    body[0].X = random.Next(1, Game.WIDTH);
                    body[0].Y = random.Next(1, Game.HEIGHT);

                    for(int i = 0; i < snake.body.Count; i++)
                    {
                        if((this.body[0].X == snake.body[i].X) && (this.body[0].Y == snake.body[i].Y))
                        {
                            goto start;
                        }
                    }

                    for(int i = 0; i < wall.body.Count; i++)
                    {
                        if((this.body[0].X == wall.body[i].X) && (this.body[0].Y == wall.body[i].Y))
                        {
                            goto start;
                        }
                    }
                    break;
                }
            }
            Draw();
        }
    }
}