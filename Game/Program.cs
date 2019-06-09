using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {

        // Screen
        static int screenWidth = 800;
        static int screenHeight = 608;

        // Time
        static float dTime;
        static float timeLastFrame = 0;
        static DateTime timeInit = DateTime.Now;
        static int MS_PER_FRAME = 30;
        static TimeSpan timeSinceInit;
        static double timeElapsed;

        public static float DTime { get => dTime; set => dTime = value; }
        public static int ScreenHeight { get => screenHeight; set => screenHeight = value; }
        public static int ScreenWidth { get => screenWidth; set => screenWidth = value; }

        static Level1 level1 = new Level1(25, 19);

        static void Main(string[] args)
        {
            int timeSleep;
            Engine.Initialize("Game", ScreenWidth, screenHeight);



            while(true)
            {
                float start = TimeController();

                Update();
                Render();

                timeSleep = TimeSleep(start);
            }
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