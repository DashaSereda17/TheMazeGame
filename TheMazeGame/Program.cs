﻿using System;
using TheMazeGame.ConsoleHelpers;
using TheMazeGame.Enums;
using TheMazeGame.Models;
using TheMazeGame.Models.GameObjects;
using TheMazeGame.TextHelpers;

namespace TheMazeGame
{
    class Program
    {
        public static GameObject PlayerInfo { get; set; }
        public static GameField GameField { get; set; }
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
            GameField = new GameField();
            Drawer = new Drawer(GameField.Cells);
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
                        Drawer.SetPlayer(PlayerInfo);
                        Drawer.Draw();
                        ConsoleHelper.ShowPlayerLifePoints((PlayerInfo as Player).CountLifePoints, Player.MAX_LIFE_POINTS);
                        Console.SetCursorPosition(PlayerInfo.PositionLeft, PlayerInfo.PositionTop);
                        (PlayerInfo as Player).StarTime = DateTime.Now;
                        MovementHandler();
                        ShowGameInfo();
                        break;
                    case MenuItemType.LoadGame:
                        if (!(PlayerInfo as Player).Load() | !GameField.Load())
                        {
                            Console.WriteLine("The version of last saved game is not available");
                            Console.ReadLine();
                        }
                        else
                        {
                            isNeedRepeatMenu = false;
                            Drawer.SetPoints(GameField.Cells);
                            Drawer.SetPlayer(PlayerInfo);
                            Drawer.Draw();
                            ConsoleHelper.ShowPlayerLifePoints((PlayerInfo as Player).CountLifePoints, Player.MAX_LIFE_POINTS);
                            Console.SetCursorPosition(PlayerInfo.PositionLeft, PlayerInfo.PositionTop);
                            MovementHandler();
                            ShowGameInfo();
                        }
                        break;
                    case MenuItemType.EditPlayer:
                        Console.WriteLine("Write your name:");
                        (PlayerInfo as Player).PlayerName = Console.ReadLine();
                        ConsoleHelper.PlayerName = (PlayerInfo as Player).PlayerName;
                        GameInfo.PlayerName = (PlayerInfo as Player).PlayerName;
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
            var nextPointType = FieldTypes.Route;
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
                                nextPointType = (GameField[Console.CursorTop + 1, Console.CursorLeft] as Cell).FieldType;
                                var canDoNextStep = NextStepHandler(nextPointType, Console.CursorTop + 1, Console.CursorLeft);
                                if (canDoNextStep)
                                {
                                    Console.CursorTop++;
                                    (PlayerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                    isNextStepDone = true;
                                }
                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            (PlayerInfo as Player).IncreaseSteps();

                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;
                            if (Console.CursorTop > 0)
                            {
                                nextPointType = (GameField[Console.CursorTop - 1, Console.CursorLeft] as Cell).FieldType;
                                if (NextStepHandler(nextPointType, Console.CursorTop - 1, Console.CursorLeft))
                                {
                                    Console.CursorTop--;
                                    (PlayerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                    isNextStepDone = true;
                                }
                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            (PlayerInfo as Player).IncreaseSteps();
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;
                            if (Console.CursorLeft > 0)
                            {
                                nextPointType = (GameField[Console.CursorTop, Console.CursorLeft - 1] as Cell).FieldType;
                                if (NextStepHandler(nextPointType, Console.CursorTop, Console.CursorLeft - 1))
                                {
                                    Console.CursorLeft--;
                                    (PlayerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                    isNextStepDone = true;
                                }
                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            (PlayerInfo as Player).IncreaseSteps();
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            Console.CursorLeft -= 1;
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;
                            if (Console.CursorLeft < Configuration.COLUMN_NUMBER - 1)
                            {
                                nextPointType = (GameField[Console.CursorTop, Console.CursorLeft + 1] as Cell).FieldType;
                                if (NextStepHandler(nextPointType, Console.CursorTop, Console.CursorLeft + 1))
                                {
                                    Console.CursorLeft++;
                                    (PlayerInfo as Player).SetPosition(Console.CursorTop, Console.CursorLeft);
                                    isNextStepDone = true;
                                }

                            }
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            (PlayerInfo as Player).IncreaseSteps();
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            TypeFinishGame = TypeFinishGame.Exit;
                            isExit = true;
                            break;
                        }
                    case ConsoleKey.F2:
                        {
                            SaveGame();
                            Console.CursorLeft -= 1;
                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
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
                    (nextPointType == FieldTypes.OpenedDoor || nextPointType == FieldTypes.ClosedDoor))
                {
                    TypeFinishGame = TypeFinishGame.Won;
                    isExit = true;
                }
                else if (isNextStepDone && (nextPointType == FieldTypes.Trap || nextPointType == FieldTypes.DeadlyTrap)
                                        && (PlayerInfo as Player).CountLifePoints == 0)
                {
                    TypeFinishGame = TypeFinishGame.Lost;
                    isExit = true;
                }
                else if (isNextStepDone && nextPointType == FieldTypes.Portal
                                        && GameField[Console.CursorTop, Console.CursorLeft].IsActive)
                {
                    GameField[Console.CursorTop, Console.CursorLeft].IsActive = false;
                    var random = new Random();
                    while (true)
                    {
                        var row = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                        var column = random.Next(0, Configuration.COLUMN_NUMBER - 1);
                        if ((GameField[row, column] as Cell).FieldType == FieldTypes.Route)
                        {
                            Drawer.DrawRoute();
                            Console.CursorLeft -= 1;

                            Console.SetCursorPosition(column, row);
                            (PlayerInfo as Player).SetPosition(row, column);

                            Drawer.DrawPlayer();
                            Console.CursorLeft -= 1;
                            break;
                        }

                    }
                }
            }
        }

        private static void SaveGame()
        {
            GameField.Save();
            (PlayerInfo as Player).Save();
        }

        static bool NextStepHandler(FieldTypes fieldTypes, int nextRowPosition, int nextColumnPosition)
        {
            var result = true;
            var points = GameField.Cells;
            switch (fieldTypes)
            {
                case FieldTypes.OpenedDoor:
                    if ((PlayerInfo as Player).CountCoins < Configuration.POINTS_TO_EXIT
                        || (PlayerInfo as Player).CountSteps > Configuration.STEPS_TO_CLOSE_DOOR)
                    {
                        result = false;
                    }

                    break;
                case FieldTypes.Wall:
                    result = false;
                    break;
                case FieldTypes.ClosedDoor:
                    result = (PlayerInfo as Player).CountKeys > 0;
                    break;
                case FieldTypes.Coin:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        (PlayerInfo as Player).IncreaseCoins();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                    }

                    break;
                case FieldTypes.Key:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        (PlayerInfo as Player).IncreaseKeys();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                    }

                    break;
                case FieldTypes.Trap:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        (PlayerInfo as Player).DecreaseLifePoints();
                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        var currentRow = Console.CursorTop;
                        var currentColumn = Console.CursorLeft;
                        ConsoleHelper.ShowPlayerLifePoints((PlayerInfo as Player).CountLifePoints,
                            Player.MAX_LIFE_POINTS);
                        Console.SetCursorPosition(currentColumn, currentRow);
                    }

                    break;
                case FieldTypes.DeadlyTrap:
                    if (points[nextRowPosition, nextColumnPosition].IsActive)
                    {
                        for (int i = 0; i < Player.MAX_LIFE_POINTS; i++)
                        {
                            (PlayerInfo as Player).DecreaseLifePoints();
                        }

                        points[nextRowPosition, nextColumnPosition].IsActive = false;
                        var currentRow = Console.CursorTop;
                        var currentColumn = Console.CursorLeft;
                        ConsoleHelper.ShowPlayerLifePoints((PlayerInfo as Player).CountLifePoints,
                            Player.MAX_LIFE_POINTS);
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
                    var time = (int)(DateTime.Now - (PlayerInfo as Player).StarTime).TotalSeconds;
                    Console.WriteLine(GameInfo.GetFinalResult((PlayerInfo as Player).CountCoins,
                        (PlayerInfo as Player).CountLifePoints, (PlayerInfo as Player).CountKeys, (PlayerInfo as Player).CountSteps, time));
                    break;
                case TypeFinishGame.Lost:
                    Console.Clear();
                    Console.WriteLine(GameInfo.GetLoseInfo((int)(DateTime.Now - (PlayerInfo as Player).StarTime).TotalSeconds));
                    break;
                case TypeFinishGame.Exit:
                    Console.Clear();
                    Console.WriteLine(GameInfo.GetExitInfo());
                    break;
            }
        }
    }
}
