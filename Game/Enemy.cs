using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Enemy : GameObject, IUpdateable, IRenderizable
    {

        Collider collider;

        public Enemy(Vector2D position, float angle) : base(position, angle)
        {
            Collider = new Collider(position.X, position.Y, 16, 16, 16, 16, true, false);
        }

        public Collider Collider { get => collider; set => collider = value; }

        public void Render()
        {
            Engine.Draw(@"img\sprites\enemy_tiny\idle_1.png", position.X, position.Y, 1, 1, angle, 16, 16);
            Collider.DrawCollider();
        }

        public void Update()
        {
            collider.X = position.X;
            collider.Y = position.Y;
        }
    }
}
