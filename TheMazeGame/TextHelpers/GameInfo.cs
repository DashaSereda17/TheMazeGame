using System;
using System.Text;

namespace TheMazeGame.TextHelpers
{
    class GameInfo
    {
        private StringBuilder textInfo;

        public GameInfo()
        {
            textInfo = new StringBuilder();
        }

        public StringBuilder GetFinalResult(int coins, int lifePoints, int keys, int steps)
        {
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine("*         Congratulation!         *");
            textInfo.AppendLine("*          You have won!          *");
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine();
            textInfo.AppendLine("Game information: ");
            textInfo.AppendLine($"Coins: {coins}");
            textInfo.AppendLine($"Life points: {lifePoints}");
            textInfo.AppendLine($"Keys: {keys}");
            textInfo.AppendLine($"Steps: {steps}");

            return textInfo;
        }

        public StringBuilder GetExitInfo()
        {
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine("*         See you later!          *");
            textInfo.AppendLine("*              Bye!               *");
            textInfo.AppendLine(new String('*', 35));

            return textInfo;
        }

        public StringBuilder GetGameMenu()
        {
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine("*              Play                *");
            textInfo.AppendLine("*         Set player name          *");
            textInfo.AppendLine("*           Instruction            *");
            textInfo.AppendLine("*              Exit                *");
            textInfo.AppendLine(new String('*', 35));

            return textInfo;
        }

        public StringBuilder GetInstruction()
        {
            textInfo.AppendLine(new String('*', 35));
            textInfo.AppendLine("Controls: ");
            textInfo.AppendLine("To move just use keys UP, DOWN, LEFT, RIGHT");
            textInfo.AppendLine("To exit the game just press key ESC");

            textInfo.AppendLine("Game information: ");
            textInfo.AppendLine("Collect coins and keys. Keys help you to open the closed door.");
            textInfo.AppendLine("Be careful there are several traps which take of your life points.");
            textInfo.AppendLine("We count all your steps and you will be able to see this info in the end of the game");
            textInfo.AppendLine(new String('*', 35));

            return textInfo;
        }
    }
}
