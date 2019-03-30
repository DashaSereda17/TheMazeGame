using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TheMazeGame.ConsoleHelpers;
using TheMazeGame.Enums;
using TheMazeGame.Models;
using TheMazeGame.TextHelpers;

namespace TheMaze
{
    class Program
    {
        public static Player PlayerInfo { get; set; }
        public static PointsChecker PointsChecker { get; set; }
        public static PointsBuilder PointsBuilder { get; set; }
        public static Drawer Drawer { get; set; }
        public static GameInfo GameInfo { get; set; }
        public static TypeFinishGame TypeFinishGame { get; set; }
        public static ConsoleHelper ConsoleHelper { get; set; }

        static void Main(string[] args)
        {
            Initialize();
            MenuHandler();

            Console.ReadKey();
        }

        public static void Initialize()
        {
            Console.CursorVisible = false;
            Console.Title = Configuration.TITLE;

            PlayerInfo = new Player();
            PointsBuilder = new PointsBuilder();
            Drawer = new Drawer(PointsBuilder.GetPoints());
            PointsChecker = new PointsChecker(PointsBuilder.GetPoints());
            GameInfo = new GameInfo();
            ConsoleHelper = new ConsoleHelper();
        }

        public static void MenuHandler()
        {
            var isNeedRepeatMenu = true;
            while (isNeedRepeatMenu)
            {
                var key = ConsoleHelper.GameMenuHandler(GameInfo.GetGameMenu());
                Console.Clear();
                switch (key)
                {
                    case MenuItemType.Play:
                        isNeedRepeatMenu = false;
                        Drawer.Draw();
                        ConsoleHelper.ShowPlayerLifePoints(PlayerInfo.CountLifePoints, Player.MAX_LIFE_POINTS);
                        Console.SetCursorPosition(0, 0);
                        MovementHandler();
                        ShowGameInfo();
                        break;
                    case MenuItemType.EditPlayer:
                        Console.WriteLine("Write your name:");
                        PlayerInfo.PlayerName = Console.ReadLine();
                        ConsoleHelper.PlayerName = PlayerInfo.PlayerName;
                        GameInfo.PlayerName = PlayerInfo.PlayerName;
                        break;
                    case MenuItemType.Information:
                        Console.WriteLine(GameInfo.GetInstruction());
                        Console.ReadKey();
                        break;
                    case MenuItemType.Exit:
                        isNeedRepeatMenu = false;
                        Console.WriteLine(GameInfo.GetExitInfo());
                        break;
                }
            }
        }

        static void MovementHandler()
        {
            var isExit = false;
            var nextPointType = PointTypes.Route;
            var isNextStepDone = false;
            while (!isExit)
            {
                var key = Console.ReadKey();
                isNextStepDone = false;
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;
                            if (Console.CursorTop < Configuration.ROW_NUMBER - 1)
                            {
                                nextPointType = PointsChecker.GetPointType(Console.CursorTop + 1, Console.CursorLeft);
                                var canDoNextStep = NextStepHandler(nextPointType, Console.CursorTop + 1, Console.CursorLeft);
                                if (canDoNextStep)
                                {
                                    Console.CursorTop++;
                                    isNextStepDone = true;
                                }
                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            PlayerInfo.IncreaseSteps();

                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;
                            if (Console.CursorTop > 0)
                            {
                                nextPointType = PointsChecker.GetPointType(Console.CursorTop - 1, Console.CursorLeft);
                                if (NextStepHandler(nextPointType, Console.CursorTop - 1, Console.CursorLeft))
                                {
                                    Console.CursorTop--;
                                    isNextStepDone = true;
                                }
                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            PlayerInfo.IncreaseSteps();
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;
                            if (Console.CursorLeft > 0)
                            {
                                nextPointType = PointsChecker.GetPointType(Console.CursorTop, Console.CursorLeft - 1);
                                if (NextStepHandler(nextPointType, Console.CursorTop, Console.CursorLeft - 1))
                                {
                                    Console.CursorLeft--;
                                    isNextStepDone = true;
                                }
                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            PlayerInfo.IncreaseSteps();
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;
                            if (Console.CursorLeft < Configuration.COLUMN_NUMBER - 1)
                            {
                                nextPointType = PointsChecker.GetPointType(Console.CursorTop, Console.CursorLeft + 1);
                                if (NextStepHandler(nextPointType, Console.CursorTop, Console.CursorLeft + 1))
                                {
                                    Console.CursorLeft++;
                                    isNextStepDone = true;
                                }

                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            PlayerInfo.IncreaseSteps();
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            TypeFinishGame = TypeFinishGame.Exit;
                            isExit = true;
                            break;
                        }
                    default:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            break;
                        }
                }

                if (isNextStepDone &&
                    (nextPointType == PointTypes.OpenedDoor || nextPointType == PointTypes.ClosedDoor))
                {
                    TypeFinishGame = TypeFinishGame.Won;
                    isExit = true;
                }
                else if (isNextStepDone && nextPointType == PointTypes.Trap && PlayerInfo.CountLifePoints == 0)
                {
                    TypeFinishGame = TypeFinishGame.Lost;
                    isExit = true;
                }
            }
        }

        static bool NextStepHandler(PointTypes pointType, int nextRowPosition, int nextColumnPosition)
        {
            var result = true;
            var points = PointsBuilder.GetPoints();
            switch (pointType)
            {
                case PointTypes.Wall:
                    result = false;
                    break;
                case PointTypes.ClosedDoor:
                    result = PlayerInfo.CountKeys > 0;
                    break;
                case PointTypes.Coin:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        PlayerInfo.IncreaseCoins();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                    }
                    break;
                case PointTypes.Key:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        PlayerInfo.IncreaseKeys();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                    }

                    break;
                case PointTypes.Trap:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        PlayerInfo.DecreaseLifePoints();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        var currentRow = Console.CursorTop;
                        var currentColumn = Console.CursorLeft;
                        ConsoleHelper.ShowPlayerLifePoints(PlayerInfo.CountLifePoints, Player.MAX_LIFE_POINTS);
                        Console.SetCursorPosition(currentColumn, currentRow);
                    }
                    break;
            }

            return result;
        }

        static void ShowGameInfo()
        {
            switch (TypeFinishGame)
            {
                case TypeFinishGame.Won:
                    Console.Clear();
                    Console.WriteLine(GameInfo.GetFinalResult(PlayerInfo.CountCoins,
                        PlayerInfo.CountLifePoints, PlayerInfo.CountKeys, PlayerInfo.CountSteps));
                    break;
                case TypeFinishGame.Lost:
                    Console.Clear();
                    Console.WriteLine(GameInfo.GetLoseInfo());
                    break;
                case TypeFinishGame.Exit:
                    Console.Clear();
                    Console.WriteLine(GameInfo.GetExitInfo());
                    break;
            }
        }
    }
}