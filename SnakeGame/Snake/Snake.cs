using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.IO;
using System.Xml.Serialization;

namespace SnakeSpace
{
    public class Snake : GameObject
    {
        public Point head;
        public int Dx { get; set; }
        public int Dy { get; set; }

        public Snake() { }

        public Snake(char sign, ConsoleColor color) : base(sign, color)
        {
            Dx = 1; 
            head = new Point { X = Game.WIDTH / 2, Y = Game.HEIGHT / 2 };
            body.Add(head);
        }

        public void ChangeDirection(int dx, int dy)
        {
            Dx = dx;
            Dy = dy;
        }

        public void Move()
        {
            Clear();

            for(int i = body.Count - 1; i > 0; i--)
            {
                body[i].X = body[i - 1].X;
                body[i].Y = body[i - 1].Y;
            }

            body[0].X += Dx;
            body[0].Y += Dy;
        }

        public void Increase(Point point)
        {
            body.Add(new Point { X = point.X, Y = point.Y });
        }

        public void Save(string fileName)
        {
            string path = @"C:\Programming\ICT\SnakeGame\Snake\bin\Debug\netcoreapp3.1\" + fileName + ".xml";
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            using(FileStream fs = new FileStream(fileName + ".xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Snake));
                xs.Serialize(fs, this);
            }
        }

        public static Snake Load(string fileName)
        {
            Snake snakeWrap;

            using(FileStream fs = new FileStream(fileName + ".xml", FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Snake));
                snakeWrap = xs.Deserialize(fs) as Snake;
            }

            return snakeWrap;
        }
    }
}