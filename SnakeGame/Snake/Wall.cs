using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnakeSpace
{
    public class Wall : GameObject
    {
        public Wall() { }

        public Wall(char sign, ConsoleColor color, string path) : base(sign, color)
        {
            lock(this)
            {
                using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        int rowIndex = 0;

                        while(!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();

                            for(int columnIndex = 0; columnIndex < line.Length; columnIndex++)
                            {
                                if(line[columnIndex] == '#')
                                {
                                    body.Add(new Point { X = columnIndex, Y = rowIndex });
                                }
                            }

                            rowIndex++;
                        }
                    }
                }

                Draw();
            }
        }
    }
}
