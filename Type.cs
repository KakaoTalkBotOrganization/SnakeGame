using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    struct Vector2
    {
        public int x, y;
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }
        /*
        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return (v1.x == v2.x) && (v1.y == v2.y);
        }
        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1.x != v2.x) || (v1.y != v2.y);
        }
        */
        public static Vector2 Up()
        {
            return new Vector2(0, 1);
        }
        public static Vector2 Down()
        {
            return new Vector2(0, -1);
        }
        public static Vector2 Right()
        {
            return new Vector2(1, 0);
        }
        public static Vector2 Left()
        {
            return new Vector2(-1, 0);
        }
        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
    }

    struct Map
    {
        public bool isSnake;
        public bool isApple;
        public Map(bool isSnake, bool isApple)
        {
            this.isSnake = isSnake;
            this.isApple = isApple;
        }
    }

    class Snake
    {
        //public int errorNumber; //에러넘버
        readonly int X_SIZE;
        readonly int Y_SIZE;

        public bool gameOver = false;

        Queue<Vector2> bodyPosition;
        Vector2 currentPosition;
        Vector2 direction;

        Random r = new Random();
        bool eat = false;

        Map[,] map;

        public Snake(int xSize, int ySize)
        {
            X_SIZE = xSize;
            Y_SIZE = ySize;
            bodyPosition = new Queue<Vector2>();
            direction = Vector2.Right();
            currentPosition = Vector2.Zero();
            bodyPosition.Enqueue(currentPosition);
            map = new Map[Y_SIZE, X_SIZE];

            for (int i = 0; i < Y_SIZE; ++i)
                for (int j = 0; j < X_SIZE; ++j)
                    map[i, j] = new Map();
        }

        public void ChangeDirection(Vector2 dir)
        {
            if (!(dir.x + direction.x == 0 && dir.y + direction.y == 0))
                direction = dir;
        }
        
        public bool Move()
        {
            currentPosition += direction;
            if (currentPosition.x == X_SIZE || currentPosition.x == -1 || currentPosition.y == Y_SIZE || currentPosition.y == -1 || map[currentPosition.y, currentPosition.x].isSnake)
            {
                //errorNumber = 2;
                gameOver = true;
                return false;
            }
            if (!map[currentPosition.y, currentPosition.x].isApple)
            {
                Vector2 tempPos = bodyPosition.Dequeue();
                map[tempPos.y, tempPos.x].isSnake = false;
            }
            else
            {
                eat = false;
                map[currentPosition.y, currentPosition.x].isApple = false;
            }
            bodyPosition.Enqueue(currentPosition);
            map[currentPosition.y, currentPosition.x].isSnake = true;
            if (bodyPosition.Count == X_SIZE * Y_SIZE)
            {
                //errorNumber = 1;
                return true;
            }
            //errorNumber = 0;
            return false;
        }

        public void RandomApple()
        {
            if (eat)
                return;
            int i = r.Next(0, Y_SIZE);
            int j = r.Next(0, X_SIZE);
            if (!(map[i, j].isSnake || map[i, j].isApple))
            {
                map[i, j].isApple = true;
                eat = true;
            }
            else
                RandomApple();
        }

        public void Print()
        {
            for (int i = 0; i < Y_SIZE; ++i)
            {
                for (int j = 0; j < X_SIZE; ++j)
                    if (map[i, j].isSnake)
                        Console.Write("@");
                    else if (map[i, j].isApple)
                        Console.Write("$");
                    else
                        Console.Write("-");
                Console.WriteLine();
            }
        }
    }
}
