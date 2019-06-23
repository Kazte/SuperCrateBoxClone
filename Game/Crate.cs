using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Crate : GameObject, IUpdateable, IRenderizable
    {

        string sprite = @"img\weapons\crate.png";
        Collider collider;
        Player player;
        Tilemap tilemap;

        bool destroyed;
        private float yspd;
        private bool ground;
        private float gravity = 1.5f;
        private float yspdMax = 30;

        public event SimpleEventHandler<Crate> OnDeactivate;

        public string Sprite { get => sprite; set => sprite = value; }
        public Collider Collider { get => collider; set => collider = value; }
        public bool Destroyed { get => destroyed; set => destroyed = value; }


        public Crate(Vector2D position, float angle) : base(position, angle)
        {
            Collider = new Collider(position.X, position.Y, 24, 24, 12, 12, true, false);
        }

        public void Init(float x, float y, Tilemap tilemap, Player player)
        {
            position.X = x;
            position.Y = y;
            this.tilemap = tilemap;
            this.player = player;
            Destroyed = false;
            Program.Crates.Add(this);
        }

        public void Desactivate()
        {
            Destroyed = true;
            Program.Crates.Remove(this);

            if (OnDeactivate != null)
            {
                OnDeactivate.Invoke(this);
            }
        }

        

        public void Render()
        {
            Engine.Draw(sprite, position, 1, 1, angle, 12, 12);
        }

        public void Update()
        {
            Movement();

            if (Collider.CheckCollision(collider, player.Collider))
            {
                GameMananger.Score++;
            }
        }

        private void Gravity()
        {
            for (int i = 0; i < Math.Abs(yspd); i++)
            {
                if (yspd > 0)
                {
                    if (TileID(position.X - collider.SizeX + 4, position.Y + collider.SizeY) != -1 || TileID(position.X + collider.SizeX - 4, position.Y + collider.SizeY) != -1)
                    {
                        yspd = 0;
                        break;
                    }
                }
                position.Y += yspd * Program.DTime;
            }

        }


        private int TileID(float x, float y)
        {
            int row = (int)(y / 32);
            int col = (int)(x / 32);
            if (col > tilemap.Tiledata.GetLength(1) - 1) { col = tilemap.Tiledata.GetLength(1) - 1; }
            if (col < 0) { col = 0; }
            if (row > tilemap.Tiledata.GetLength(0) - 1) { row = tilemap.Tiledata.GetLength(0) - 1; }
            if (row < 0) { row = 0; }
            return tilemap.Tiledata[row, col];
        }

        private void Movement()
        {
            if (TileID(position.X - collider.SizeX + 4, position.Y + collider.SizeY) != -1 || TileID(position.X + collider.SizeX - 4, position.Y + collider.SizeY) != -1)
            {
                ground = true;
            }
            else
            {
                ground = false;
            }

            if (!ground)
            {
                yspd = Utils.Focus(yspd, yspdMax, gravity);
            }

            Gravity();
        }
    }
}
