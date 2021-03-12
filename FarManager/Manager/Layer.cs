using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Manager
{
    class Layer
    {
        public DirectoryInfo DirInfo { get; set; }
        public FileInfo FilesInfo { get; set; }
        public int Position { get; set; }
        public List<FileSystemInfo> Content { get; set; }
        public Layer() { }

        public Layer(DirectoryInfo DirInfo, int Position)
        {
            this.DirInfo = DirInfo;
            this.Position = Position;
            this.Content = new List<FileSystemInfo>();
            this.Content.AddRange(this.DirInfo.GetDirectories());
            this.Content.AddRange(this.DirInfo.GetFiles());
        }

        public Layer(FileInfo FilesInfo)
        {
            this.FilesInfo = FilesInfo;
        }

        public void PrintInfo()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            int count = 0;

            foreach(DirectoryInfo di in DirInfo.GetDirectories()) 
            {
                if(count == Position)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Title = CalculateSize(di.FullName).ToString();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.WriteLine(di.Name);
                count++;
            }

            Console.ForegroundColor = ConsoleColor.DarkBlue;

            foreach(FileInfo fi in DirInfo.GetFiles())
            {
                if(count == Position)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Title = CalculateSize(fi.FullName).ToString();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.WriteLine(fi.Name);
                count++;
            }
        }

        public void PrintContent()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            string[] lines = File.ReadAllLines(FilesInfo.FullName);

            foreach(string line in lines) 
            {
                Console.WriteLine(line);
            }
        }

        public FileSystemInfo GetCurrentObject()
        {
            return this.Content[Position];
        }

        public void SetNewPosition(int inc)
        {
            if(inc > 0)
            {
                Position++;
            }
            else
            {
                Position--;
            }

            if(Position >= Content.Count)
            {
                Position = 0;
            }
            else if(Position < 0)
            {
                Position = Content.Count - 1;
            }
        }

        static double CalculateSize(string folder)
        {
            double size = 0;
            
            if (!Directory.Exists(folder))
            {
                return -1;
            }
            else
            {
                try
                {
                    foreach (string file in Directory.GetFiles(folder))
                    {
                        if (File.Exists(file))
                        {
                            FileInfo fi = new FileInfo(file);
                            size += fi.Length;
                        }
                    }
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return size;
        }
    }
}
