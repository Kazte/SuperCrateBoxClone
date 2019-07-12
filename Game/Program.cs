using Game.screens;
using Game.weapons;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        
        // Random
        public static Random random = new Random();

        // Screen
        static int screenWidth = 800;
        static int screenHeight = 608;
        static Screen actualScreen;

        // Time
        static float dTime;
        static int timeSleep;
        static float timeLastFrame = 0;
        static DateTime timeInit = DateTime.Now;
        static int MS_PER_FRAME = 30;
        static TimeSpan timeSinceInit;

        // Press Space
        private static bool canPressSpace = true;

        // Weapons
        public static Dictionary<string, Gun> weapons { get; set; } = new Dictionary<string, Gun>();
        public static List<Bullet> Bullets { get; set; } = new List<Bullet>();
        public static List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static List<Crate> Crates { get; set; } = new List<Crate>();

        public static float DTime { get => dTime; set => dTime = value; }
        public static int ScreenHeight { get => screenHeight; set => screenHeight = value; }
        public static int ScreenWidth { get => screenWidth; set => screenWidth = value; }
        internal static Screen ActualScreen { get => actualScreen; set => actualScreen = value; }
        public static bool CanPressSpace { get => canPressSpace; set => canPressSpace = value; }
        public static Level1 Level1 { get => level1; set => level1 = value; }
        public static Level2 Level2 { get => level2; set => level2 = value; }

        static MainMenu mainMenu;
        static Level1 level1;
        static Level2 level2;
        static GameOver gameOver;

        static void Main(string[] args)
        {


            Initialize();


            while (true)
            {
                float start = TimeController();

                Update();
                Render();

                timeSleep = TimeSleep(start);
            }
        }

        private static void Initialize()
        {

            Engine.Initialize("Game", ScreenWidth, screenHeight);
            SaveMananger.Instance.LoadCsv();
            LoadWeapons();
            mainMenu = new MainMenu();
            Level1 = new Level1();
            level2 = new Level2();
            gameOver = new GameOver();
            actualScreen = Screen.level2;
        }

        private static void LoadWeapons()
        {
            weapons.Add("pistol", new Pistol(new Vector2D(0, 0), 0, 10, false, 600));
            weapons.Add("smg", new SMG(new Vector2D(0, 0), 0, 30, true, 650));
            weapons.Add("ak-47", new AK(new Vector2D(0, 0), 0, 60, true, 800));
            weapons.Add("shotgun", new Shotgun(new Vector2D(0, 0), 0, 7, false, 800));
        }

        private static int TimeSleep(float start)
        {
            int timeSleep = Convert.ToInt32(start + MS_PER_FRAME - GetCurrentTime());
            if (timeSleep > 0)
            {
                System.Threading.Thread.Sleep(timeSleep);
            }

            return timeSleep;
        }

        private static float TimeController()
        {
            timeSinceInit = DateTime.Now - timeInit;
            float start = (float)timeSinceInit.TotalSeconds;
            dTime = start - timeLastFrame;
            timeLastFrame = start;
            return start;
        }

        static float GetCurrentTime()
        {
            TimeSpan diffStart = DateTime.Now.Subtract(timeInit);
            return (float)diffStart.TotalMilliseconds;
        }

        private static void Update()
        {
            switch (actualScreen)
            {
                case Screen.main_menu:
                    mainMenu.Update();
                    break;

                case Screen.level1:
                    Level1.Update();
                    break;

                case Screen.level2:
                    Level2.Update();
                    break;
                
                case Screen.game_over:
                    gameOver.Update();
                    break;

                default:
                    break;
            }
        }

        private static void Render()
        {
            Engine.Clear();

            switch (actualScreen)
            {
                case Screen.main_menu:
                    mainMenu.Render();
                    break;

                case Screen.level1:
                    Level1.Render();
                    break;
                
                case Screen.level2:
                    Level2.Render();
                    break;

                case Screen.game_over:
                    gameOver.Render();
                    break;

                default:
                    break;
            }

            Engine.Show();
        }
    }

}