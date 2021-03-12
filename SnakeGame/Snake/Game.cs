using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace SnakeSpace
{
    public class Game
    {
        readonly Timer snakeTimer;
        readonly Timer statsTimer;

        readonly public static int WIDTH = 60;
        readonly public static int HEIGHT = 30;
        readonly public static string[] levels = {
            @"C:\Programming\ICT\SnakeGame\Snake\Levels\level1.txt",
            @"C:\Programming\ICT\SnakeGame\Snake\Levels\level2.txt",
            @"C:\Programming\ICT\SnakeGame\Snake\Levels\level3.txt"
        };

        Snake snake;
        Food food;
        Wall wall;
        static int eatenFood;
        static int wallLevel;

        public bool IsRunning { get; set; }

        public Game()
        {
            snake = new Snake('@', ConsoleColor.Green);
            food = new Food('O', ConsoleColor.Red);
            wall = new Wall('#', ConsoleColor.White, levels[0]);

            IsRunning = true;

            Console.CursorVisible = false;
            Console.SetWindowSize(Game.WIDTH, Game.HEIGHT);
            Console.SetBufferSize(Game.WIDTH, Game.HEIGHT);

            eatenFood = 0;
            wallLevel = 1;

            snakeTimer = new Timer(90);
            statsTimer = new Timer(1000);

            snakeTimer.Elapsed += Manage;
            snakeTimer.Start();
            statsTimer.Elapsed += ChangeStats;
            statsTimer.Start();
        }

        private void ChangeStats(object sender, ElapsedEventArgs e)
        {
            Console.Title = DateTime.Now.ToLongTimeString() + ". Eaten food: " + eatenFood;
        }

        public bool CheckFoodSnakeCollision()
        {
            return (snake.body[0].X == food.body[0].X && snake.body[0].Y == food.body[0].Y);
        }

        public bool CheckSnakeWallCollision()
        {
            for(int i = 0; i < wall.body.Count; i++)
            {
                if((wall.body[i].X == snake.body[0].X) && (wall.body[i].Y == snake.body[0].Y))
                {
                    return true;
                }
            }
            return false;
        }

        public void Manage(object sender, ElapsedEventArgs e)
        {
            snake.Move();
            snake.Draw();

            if(CheckSnakeWallCollision())
            {
                IsRunning = false;
                Environment.Exit(0);
            }

            if(CheckFoodSnakeCollision())
            {
                eatenFood++;
                if(eatenFood == 3 && wallLevel == 1)
                {
                    wall = new Wall('#', ConsoleColor.White, levels[1]);
                    wallLevel++;
                    snakeTimer.Interval = 70;
                    RespawnSnake(snake.body.Count);
                }
                else if(eatenFood == 5 && wallLevel == 2)
                {
                    wall = new Wall('#', ConsoleColor.White, levels[2]);
                    snakeTimer.Interval = 50;
                    RespawnSnake(snake.body.Count);
                }

                snake.Increase(snake.body[0]);
                food.Generate(snake, wall);
            }
        }

        public void RespawnSnake(int snakeSize)
        {
            snake.Clear();
            snakeTimer.Stop();
            snake = new Snake('@', ConsoleColor.Green);
            for(int i = 0; i < snakeSize - 1; i++)
            {
                snake.Increase(snake.body[0]);
            }
            snakeTimer.Start();
        }

        public void KeyPressed(ConsoleKeyInfo key)
        {
            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    snake.ChangeDirection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    snake.ChangeDirection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    snake.ChangeDirection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    snake.ChangeDirection(1, 0);
                    break;
                case ConsoleKey.Spacebar:
                    if(snakeTimer.Enabled)
                    {
                        snakeTimer.Stop();
                    }
                    else
                    {
                        snakeTimer.Start();
                    }
                    break;
                case ConsoleKey.S:
                    snake.Save("snake_data");
                    break;
                case ConsoleKey.L:
                    snake.Clear();
                    snakeTimer.Stop();
                    snake = Snake.Load("snake_data");
                    snakeTimer.Start();
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    snakeTimer.Stop();
                    break;
            }
        }
    }
}