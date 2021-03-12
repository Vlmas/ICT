using System;
using System.Collections.Generic;

namespace SnakeSpace
{
    public abstract class GameObject
    {
        public char sign;
        public ConsoleColor color;
        public List<Point> body;
        public object locker;

        public GameObject() { }

        public GameObject(char sign, ConsoleColor color)
        {
            this.locker = new object();
            this.sign = sign;
            this.color = color;
            this.body = new List<Point>();
        }

        public void Draw()
        {
            lock(locker)
            {
                Console.ForegroundColor = color;

                for(int i = 0; i < body.Count; i++)
                {
                    Console.SetCursorPosition(body[i].X, body[i].Y);
                    Console.Write(sign);
                }
            }
        }

        public void Clear()
        {
            lock(locker)
            {
                for(int i = 0; i < body.Count; i++)
                {
                    Console.SetCursorPosition(body[i].X, body[i].Y);
                    Console.Write(' ');
                }
            }
        }

        public List<Point> GetBody()
        {
            return body;
        }
    }
}