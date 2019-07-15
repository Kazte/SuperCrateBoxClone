namespace Game
{
    public class GameObject
    {

        protected string sprite;

        public string Sprite { get => sprite; set => sprite = value; }
        public Vector2D Position { get => position; set => position = value; }

        protected Vector2D position = Vector2D.zero();
        protected float angle;



        public GameObject(Vector2D position, float angle)
        {
            this.position = position;
            this.angle = angle;
        }

        public GameObject()
        {
            
        }





    }
}
