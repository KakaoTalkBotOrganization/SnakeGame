using SnakeGame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static Snake snake;
        static void Main(string[] args)
        {
            snake = new Snake(4,4);
            Thread thread = new Thread(() => KeyPressEvent());
            thread.Start();
            while (!snake.gameOver)
            {
                if (snake.Move())
                    break;
                snake.RandomApple();
                if (!snake.gameOver)
                    snake.Print();
                Thread.Sleep(500);
                Console.Clear();
            }
            if (!snake.gameOver)
                Console.WriteLine("You win");
            else
                Console.WriteLine("game over");
        }

        static void KeyPressEvent()
        {
            while (!snake.gameOver)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        snake.ChangeDirection(Vector2.Down());
                        break;
                    case ConsoleKey.DownArrow:
                        snake.ChangeDirection(Vector2.Up());
                        break;
                    case ConsoleKey.RightArrow:
                        snake.ChangeDirection(Vector2.Right());
                        break;
                    case ConsoleKey.LeftArrow:
                        snake.ChangeDirection(Vector2.Left());
                        break;
                }
            }
        }
    }
}
