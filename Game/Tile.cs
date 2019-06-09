using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Tile: IRenderizable, IUpdateable
    {
        string sprite;
        int id;
        Vector2D position;
        Collider collider;

        

        public string Sprite { get => sprite; set => sprite = value; }
        public Vector2D Position { get => position; set => position = value; }
        public int Id { get => id; set => id = value; }

        public Tile(string sprite, Vector2D position, int id = -1)
        {
            this.sprite = sprite;
            this.position = position;
            this.Id = id;

            collider = new Collider(position.X, position.Y, 32, 32, 16, 16, true, false);
        }

        public Tile(string sprite, int id = -1)
        {
            this.sprite = sprite;
            this.Id = id;

            collider = new Collider(position.X, position.Y, 32, 32, 16, 16, true, false);
        }

        public void Update()
        {
            collider.X = position.X;
            collider.Y = position.Y;
        }

        public void Render()
        {
            
            Engine.Draw(sprite, position, 1, 1, 0, 16, 16);
            //collider.DrawCollider();
        }
    }
}
