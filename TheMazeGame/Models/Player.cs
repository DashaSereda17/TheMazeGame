namespace TheMazeGame.Models
{
    public class Player
    {
        public const int MAX_LIFE_POINTS = 3;

        public int CountLifePoints { get; private set; } = MAX_LIFE_POINTS;
        public int CountSteps { get; private set; }
        public int CountKeys { get; private set; }
        public int CountCoins { get; private set; }
        public string PlayerName { get; set; }

        public void IncreaseLifePoints()
        {
            CountLifePoints = CountLifePoints + 1 <= MAX_LIFE_POINTS ? ++CountLifePoints : CountLifePoints;
        }

        public void DecreaseLifePoints()
        {
            CountLifePoints = CountLifePoints - 1 >= 0 ? --CountLifePoints : CountLifePoints;
        }

        public void IncreaseSteps()
        {
            CountSteps++;
        }

        public void IncreaseKeys()
        {
            CountKeys++;
        }

        public void IncreaseCoins()
        {
            CountCoins++;
        }
    }
}
