using System;
using TheMazeGame.Enums;

namespace TheMazeGame.Models
{
    public struct Point
    {
        public ConsoleColor ColorForground { get; set; }
        public ConsoleColor ColorBackground { get; set; }
        public PointTypes PointType { get; set; }
        public bool IsActive { get; set; }
        public char Symbol { get; set; }
    }
}