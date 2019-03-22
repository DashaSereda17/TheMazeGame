using System;
using TheMazeGame.ConsoleHelpers;
using TheMazeGame.Enums;
using TheMazeGame.Models;
using TheMazeGame.TextHelpers;

namespace TheMazeGame
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

            Drawer.Draw();

            Console.SetCursorPosition(0, 0);

            MovementHandler();

            ShowGameInfo();

            Console.ReadKey();
        }

        public static void Initialize()
        {
            Console.CursorVisible = false;
            Console.Title = "The Maze";

            PlayerInfo = new Player();
            PointsBuilder = new PointsBuilder();
            Drawer = new Drawer(PointsBuilder.GetPoints());
            PointsChecker = new PointsChecker(PointsBuilder.GetPoints());
            GameInfo = new GameInfo();
            ConsoleHelper = new ConsoleHelper();
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
                                var canDoNextStep = NextStepHandler(nextPointType);
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
                                if (NextStepHandler(nextPointType))
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
                                if (NextStepHandler(nextPointType))
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
                                if (NextStepHandler(nextPointType))
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
                }

                if (isNextStepDone &&
                    (nextPointType == PointTypes.OpenedDoor || nextPointType == PointTypes.ClosedDoor))
                {
                    TypeFinishGame = TypeFinishGame.Won;
                    isExit = true;
                }
            }
        }

        static bool NextStepHandler(PointTypes pointType)
        {
            var result = true;
            switch (pointType)
            {
                case PointTypes.Wall:
                    result = false;
                    break;
                case PointTypes.ClosedDoor:
                    result = PlayerInfo.CountKeys > 0;
                    break;
                case PointTypes.Coin:
                    PlayerInfo.IncreaseCoins();
                    break;
                case PointTypes.Key:
                    PlayerInfo.IncreaseKeys();
                    break;
                case PointTypes.Trap:
                    PlayerInfo.DecreaseLifePoints();
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
                case TypeFinishGame.Exit:
                    Console.Clear();
                    Console.WriteLine(GameInfo.GetExitInfo());
                    break;
            }
        }
    }
}
