namespace Game.screens
{
    public class SelectLevel : Pantalla
    {
        private Button level1;
        private Button level2;
        private Button level3;

        public SelectLevel()
        {
            bgImage = "img/selectLevel_bg.png";

            level1 = new Button(330, 164, "img/btnLvl1.png");
            buttons.Add(level1);

            level2 = new Button(330, 308, "img/btnLvl2.png");
            buttons.Add(level2);

            level3 = new Button(330, 452, "img/btnLvl3.png");
            buttons.Add(level3);
            
            level1.SetButtons(level3, level2);
            level2.SetButtons(level1, level3);
            level3.SetButtons(level2, level1);

            buttonCurrent = level1;

            indicator = new Indicator(0, 0, 50f);
            indicator.MoveToPosition(buttonCurrent.X, buttonCurrent.Y);
        }

        public override void EnterButton()
        {
            if (Program.CanPressSpace)
            {
                if (buttonCurrent == level1)
                {
                    Program.Level1.ResetLevel(25, 19);
                    Program.ActualScreen = Screen.level1;
                }
                else if (buttonCurrent == level2)
                {
                    Program.Level2.ResetLevel(25, 19);
                    Program.ActualScreen = Screen.level2;
                }
                else if (buttonCurrent == level3)
                {
                    Program.Level3.ResetLevel(25, 19);
                    Program.ActualScreen = Screen.level3;
                }

                Program.CanPressSpace = false;
            }
        }
    }
}