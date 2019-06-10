using System;
using TheMaze.Core.Enums;

namespace TheMaze.Mappers
{
    public static class ConsoleColorMapper
    {
        public static ConsoleColor MapToConsoleColor(CellColor cellColor)
        {
            ConsoleColor color = ConsoleColor.Red;
            switch (cellColor)
            {
                case CellColor.WallBackground:
                    color = ConsoleColor.Yellow;
                    break;
                case CellColor.WallForeground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.CoinBackground:
                    color = ConsoleColor.Red;
                    break;
                case CellColor.CoinForeground:
                    color = ConsoleColor.DarkYellow;
                    break;
                case CellColor.ClosedDoorBackground:
                    color = ConsoleColor.Cyan;
                    break;
                case CellColor.ClosedDoorForeground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.DeadlyTrapBackground:
                    color = ConsoleColor.Black;
                    break;
                case CellColor.DeadlyTrapForeground:
                    color = ConsoleColor.DarkRed;
                    break;
                case CellColor.OpenDoorBackground:
                    color = ConsoleColor.Green;
                    break;
                case CellColor.OpenDoorForeground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.KeyBackground:
                    color = ConsoleColor.Red;
                    break;
                case CellColor.KeyForeground:
                    color = ConsoleColor.Cyan;
                    break;
                case CellColor.CrystalBackground:
                    color = ConsoleColor.Blue;
                    break;
                case CellColor.CrystalForeground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.TrapBackground:
                    color = ConsoleColor.Red;
                    break;
                case CellColor.TrapForeground:
                    color = ConsoleColor.DarkRed;
                    break;
                case CellColor.PortalBackground:
                    color = ConsoleColor.Magenta;
                    break;
                case CellColor.PortalForeground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.PrizeBackground:
                    color = ConsoleColor.Green;
                    break;
                case CellColor.PrizeForeground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.PlayerBackground:
                    color = ConsoleColor.White;
                    break;
                case CellColor.PlayerForeground:
                    color = ConsoleColor.Blue;
                    break;

            }

            return color;
        }
    }
}
