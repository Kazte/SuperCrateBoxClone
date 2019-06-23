using Game.weapons;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {

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



        // Weapons
        public static Dictionary<string, Gun> weapons = new Dictionary<string, Gun>();
        public static List<Bullet> Bullets { get; set; } = new List<Bullet>();
        public static List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static List<Crate> Crates{ get; set; } = new List<Crate>();

        public static float DTime { get => dTime; set => dTime = value; }
        public static int ScreenHeight { get => screenHeight; set => screenHeight = value; }
        public static int ScreenWidth { get => screenWidth; set => screenWidth = value; }
        internal static Screen ActualScreen { get => actualScreen; set => actualScreen = value; }

        static Level1 level1;

        static void Main(string[] args)
        {
            

            Initialize();
            

            while(true)
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
            LoadWeapons();
            level1 = new Level1();
        }

        private static void LoadWeapons()
        {
            weapons.Add("pistol", new Pistol(new Vector2D(0, 0), 0, 10, false, 600));
            weapons.Add("smg", new SMG(new Vector2D(0, 0), 0, 30, true, 650));
            weapons.Add("ak-47", new AK(new Vector2D(0, 0), 0, 60, true, 800));
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
            level1.Update();
        }

        private static void Render()
        {
            Engine.Clear();

            level1.Render();

            Engine.Show();
        }
    }

}