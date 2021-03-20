using System;
using System.Collections.Generic;
using System.IO;

namespace Manager
{
    class Program
    {
        const string PATH = @"C:\Programming";
        static void Manage(string path)
        {
            ConsoleKeyInfo keyInfo;
            bool exit = false;
            bool shareMode = false;

            Stack<Layer> history = new Stack<Layer>();
            history.Push(new Layer(new DirectoryInfo(path), 0));

            while(!exit)
            {
                try
                {
                    if (!shareMode)
                    {
                        history.Peek().PrintInfo();
                    }
                    else
                    {
                        history.Peek().PrintContent();
                    }
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine("You can't go beyond!");
                }
                keyInfo = Console.ReadKey(true);

                switch(keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (history.Peek().GetCurrentObject().GetType() == typeof(DirectoryInfo))
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObject() as DirectoryInfo, 0));
                        }
                        else if (history.Peek().GetCurrentObject().GetType() == typeof(FileInfo))
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObject() as FileInfo));
                            shareMode = true;
                        }
                        break;
                    case ConsoleKey.W:
                        history.Peek().SetNewPosition(-1);
                        break;
                    case ConsoleKey.S:
                        history.Peek().SetNewPosition(1);
                        break;
                    case ConsoleKey.Escape:
                        history.Pop();
                        shareMode = false;
                        break;
                    case ConsoleKey.N:
                        CreateDirectory();
                        break;
                    case ConsoleKey.M:
                        DeleteDirectory(Console.ReadLine());
                        break;
                    case ConsoleKey.E:
                        exit = true;
                        break;
                }
            }
        }

        public static void CreateDirectory()
        {
            Random random = new Random();
            string path = PATH + @"\" + random.Next(0, 100);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void DeleteDirectory(string name)
        {
            string path = PATH + @"\" + name;

            if(Directory.Exists(path)) 
            {
                foreach (string f in Directory.GetFiles(path))
                {
                    File.Delete(f);
                }
                foreach (string f in Directory.GetDirectories(path))
                {
                    DeleteDirectory(f);
                }
                Directory.Delete(path);
            }
        }

        static void Main(string[] args)
        {
            Manage(PATH);
        }
    }
}
