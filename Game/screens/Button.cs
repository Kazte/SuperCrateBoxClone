namespace Game.screens
{
    public class Button : Vector2D, IRenderizable
    {
        private string sprite;
        private Button buttonUp;
        private Button buttonDown;
        private Button buttonCurrent;

        private float timer = 0f;
        private float timeToPress = 0.25f;

        public Button ButtonCurrent { get => buttonCurrent; set => buttonCurrent = value; }
        public string Sprite { get => sprite; set => sprite = value; }

        public Button(float x, float y, string sprite) : base(x, y)
        {
            this.sprite = sprite;
        }

        public void Update()
        {
            timer += Program.DTime;

            buttonCurrent = GetButton();
        }

        public Button GetButton()
        {
            if (Engine.GetKey(Keys.UP) && timer >= timeToPress)
            {
                timer = 0f;
                return GetUp();
            }
            else if (Engine.GetKey(Keys.DOWN) && timer >= timeToPress)
            {
                timer = 0f;
                return GetDown();
            }
            else return this;
        }

        private Button GetDown()
        {
            if (buttonDown != null)
            {
                return buttonDown;
            }
            else return this;
        }

        private Button GetUp()
        {
            if (buttonUp != null)
            {
                return buttonUp;
            }
            else return this;
        }

        public void SetButtons(Button up, Button down)
        {
            buttonUp = up;
            buttonDown = down;
        }

        public void Render()
        {
            Engine.Draw(sprite, x, y);
        }
    }
}
