﻿using System;

namespace Game.screens
{
    public class MainMenu : Pantalla
    {
        private Button buttonStart;
        private Button buttonExit;



        public MainMenu()
        {
            bgImage = "img/mainScreen.png";

            buttonStart = new Button(300, 250, "img/newGameBtn.png");
            buttons.Add(buttonStart);

            buttonExit = new Button(300, 350, "img/exitBtn.png");
            buttons.Add(buttonExit);

            buttonStart.SetButtons(buttonExit, buttonExit);
            buttonExit.SetButtons(buttonStart, buttonStart);

            buttonCurrent = buttonStart;

            indicator = new Indicator(0, 0, 60f);
            indicator.MoveToPosition(buttonCurrent.X, buttonCurrent.Y);
        }

        public override void Render()
        {
            base.Render();
            new Text("high score " + GameMananger.HighScore, Program.ScreenWidth / 2 - 50, Program.ScreenHeight - 30, 20, 27).drawText();
            new Text("x shoot", 30, 300, 15, 20).drawText();
            new Text("z reload", 30, 325, 15, 20).drawText();
            new Text("a bombs", 30, 350, 15, 20).drawText();
        }

        public override void EnterButton()
        {
            if (buttonCurrent == buttonStart)
            {
                Program.Level1.ResetLevel(25, 19);
                Program.ActualScreen = Screen.level1;
            }
            else if (buttonCurrent == buttonExit)
            {
                Environment.Exit(1);
            }
        }
    }
}
