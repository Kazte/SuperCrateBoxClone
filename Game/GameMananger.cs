namespace Game
{
    public class GameMananger
    {
        private static GameMananger instance = null;

        private static int score;
        private static int highScore;

        protected GameMananger()
        {

        }

        public static GameMananger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameMananger();
                }
                return instance;
            }
        }

        public static int Score { get => score; set => score = value; }
        public static int HighScore { get => highScore; set => highScore = value; }
    }
}
