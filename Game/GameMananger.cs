using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameMananger
    {
        private static GameMananger instance = null;

        private static int score;

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
    }
}
