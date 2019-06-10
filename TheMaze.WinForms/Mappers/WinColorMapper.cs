using System.Drawing;
using TheMaze.Core.Enums;

namespace TheMaze.WinForms.Mappers
{
    public static class WinColorMapper
    {
        public static Color MapToWinColor(CellColor cellColor)
        {
            var color = Color.White;
            switch (cellColor)
            {
                case CellColor.WallBackground:
                    color = Color.Yellow;
                    break;
                case CellColor.WallForeground:
                    color = Color.Black;
                    break;
                case CellColor.CoinBackground:
                    color = Color.White;
                    break;
                case CellColor.CoinForeground:
                    color = Color.Orange;
                    break;
                case CellColor.ClosedDoorBackground:
                    color = Color.Cyan;
                    break;
                case CellColor.ClosedDoorForeground:
                    color = Color.Black;
                    break;
                case CellColor.DeadlyTrapBackground:
                    color = Color.White;
                    break;
                case CellColor.DeadlyTrapForeground:
                    color = Color.Red;
                    break;
                case CellColor.OpenDoorBackground:
                    color = Color.Green;
                    break;
                case CellColor.OpenDoorForeground:
                    color = Color.White;
                    break;
                case CellColor.KeyBackground:
                    color = Color.White;
                    break;
                case CellColor.KeyForeground:
                    color = Color.Cyan;
                    break;
                case CellColor.CrystalBackground:
                    color = Color.Blue;
                    break;
                case CellColor.CrystalForeground:
                    color = Color.White;
                    break;
                case CellColor.TrapBackground:
                    color = Color.White;
                    break;
                case CellColor.TrapForeground:
                    color = Color.DarkRed;
                    break;
                case CellColor.PortalBackground:
                    color = Color.Magenta;
                    break;
                case CellColor.PortalForeground:
                    color = Color.White;
                    break;
                case CellColor.PrizeBackground:
                    color = Color.White;
                    break;
                case CellColor.PrizeForeground:
                    color = Color.Green;
                    break;
                case CellColor.PlayerBackground:
                    color = Color.White;
                    break;
                case CellColor.PlayerForeground:
                    color = Color.Blue;
                    break;
            }

            return color;
        }
    }
}
