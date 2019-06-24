using System;
using System.Collections.Generic;

namespace Game.screens
{
    public class Pantalla
    {
        protected Indicator indicator;
        protected Button buttonCurrent;
        protected List<Button> buttons = new List<Button>();
        protected string bgImage;

        public virtual void Render()
        {
            Engine.Draw(bgImage);

            foreach (var button in buttons)
            {
                button.Render();
            }
            indicator.Render();
        }

        public virtual void EnterButton()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            buttonCurrent = buttonCurrent.GetButton();
            buttonCurrent.Update();
            indicator.MoveToPosition(buttonCurrent.X, buttonCurrent.Y);

            if (Engine.GetKey(Keys.SPACE))
            {
                EnterButton();
            }
        }
    }
}
