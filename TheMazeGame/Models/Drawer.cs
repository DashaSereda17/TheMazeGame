using System;

namespace TheMazeGame.Models
{
    public class Drawer
    {
        private Point[,] points;

        public Drawer(Point[,] points)
        {
            this.points = points;
        }

        public void Draw()
        {
            for (var i = 0; i < points.GetLength(0); i++)
            {
                for (int j = 0; j < points.GetLength(1); j++)
                {
                    Console.ForegroundColor = points[i, j].ColorForground;
                    Console.BackgroundColor = points[i, j].ColorBackground;
                    Console.Write(points[i, j].Symbol);
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DrawPlayer()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write('@');
        }

        public void DrawRoute()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(' ');
        }
    }
}
