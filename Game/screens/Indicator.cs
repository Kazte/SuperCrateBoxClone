namespace Game.screens
{
    public class Indicator : Vector2D, IRenderizable
    {
        private float offset;
        private string sprite;

        public string Sprite { get => sprite; set => sprite = value; }

        public Indicator(float x, float y, float offset) : base(x, y)
        {
            this.offset = offset;
            sprite = "img/selector.png";
        }

        public void MoveToPosition(float newx, float newy)
        {
            x = newx;
            y = newy;
        }

        public void Render()
        {
            Engine.Draw(sprite, x - offset, y);
        }
    }
}
