using System;

namespace Game.screens
{
    public class GameOver : Pantalla
    {
        private Button buttonRestart;
        private Button buttonExit;

        public GameOver()
        {
            bgImage = "img/gameoverScreen.png";

            buttonExit = new Button(300, 300, "img/exitBtn.png");
            buttons.Add(buttonExit);

            buttonRestart = new Button(300, 400, "img/resetBtn.png");
            buttons.Add(buttonRestart);

            buttonExit.SetButtons(buttonRestart, buttonRestart);
            buttonRestart.SetButtons(buttonExit, buttonExit);

            buttonCurrent = buttonExit;

            indicator = new Indicator(0, 0, 60f);
            indicator.MoveToPosition(buttonCurrent.X, buttonCurrent.Y);
        }

        public override void Render()
        {
            base.Render();
            new Text("score " + GameMananger.Score, 20, Program.ScreenHeight - 60, 20, 27).drawText();
            new Text("high score " + GameMananger.HighScore, 20, Program.ScreenHeight - 30, 20, 27).drawText();
        }

        public override void EnterButton()
        {
            if (buttonCurrent == buttonRestart)
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
