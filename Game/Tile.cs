namespace Game
{
    public class Tile : IRenderizable, IUpdateable
    {
        string sprite;
        int id;
        Vector2D position;
        Collider collider;



        public string Sprite { get => sprite; set => sprite = value; }
        public Vector2D Position { get => position; set => position = value; }
        public int Id { get => id; set => id = value; }
        public Collider Collider { get => collider; set => collider = value; }

        public Tile(string sprite, Vector2D position, int id = -1)
        {
            this.sprite = sprite;
            this.position = position;
            this.Id = id;

            Collider = new Collider(position.X, position.Y, 32, 32, true, false);
        }

        public Tile(string sprite, int id = -1)
        {
            this.sprite = sprite;
            this.Id = id;

            Collider = new Collider(position.X, position.Y, 32, 32, true, false);
        }

        public void Update()
        {
            Collider.X = position.X;
            Collider.Y = position.Y;
        }

        public void Render()
        {

            Engine.Draw(sprite, position, 1, 1, 0, 16, 16);
//            collider.DrawCollider();
        }
    }
}
