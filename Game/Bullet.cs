﻿using System;

namespace Game
{
    public class Bullet : GameObject, IUpdateable, IRenderizable
    {
        private float velocity = 300f;
        private float lifeTime = 3f;
        private float timer;
        Tilemap tilemap;

        public event SimpleEventHandler<Bullet> OnDeactivate;


        Collider collider;
        private float dirX;
        private float dirY;

        public bool Destroyed { get; set; }
        public float Radius { get; set; }
        public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

        public Bullet(Vector2D position, float angle) : base(position, angle)
        {
            collider = new Collider(position.X, position.Y, 8, 8, 4, 4, true, true);
        }

        public void Init(float x, float y, int face, int speed, float angle)
        {
            position.X = x;
            position.Y = y;
            velocity = speed;
            this.Tilemap = Tilemap;
            this.angle = angle;

            timer = 0;
            Destroyed = false;
            Program.Bullets.Add(this);
        }

        public void Render()
        {
            if (!Destroyed)
            {
                Engine.Draw(@"img\weapons\bullet.png", position.X, position.Y, 1, 1, 0, 4, 4);
            }
        }

        public void Update()
        {
            collider.X = position.X;
            collider.Y = position.Y;

            if (timer >= lifeTime)
            {
                Desactivate();
            }
            else if (!Destroyed)
            {

                timer += Program.DTime;
                Movement();
                CheckCollisions();
            }
        }

        public void Desactivate()
        {
            Destroyed = true;
            Program.Bullets.Remove(this);

            if (OnDeactivate != null)
            {
                OnDeactivate.Invoke(this);
            }
        }

        private void Movement()
        {
            dirX = (float)Math.Cos((angle) * Math.PI / 180);
            dirY = (float)Math.Sin((angle) * Math.PI / 180);

            position.X += dirX * velocity * Program.DTime;
            position.Y += dirY * velocity * Program.DTime;
        }

        // Funcion de chequear colisiones con enemigos
        private void CheckCollisions()
        {
            for (int i = 0; i < Tilemap.Tiledata.GetLength(0); i++)
            {
                for (int j = 0; j < Tilemap.Tiledata.GetLength(1); j++)
                {
                    if (Tilemap.GetTile(i, j) != null)
                    {
                        if (Collider.CheckCollision(Tilemap.GetTile(i, j).Collider, collider))
                        {
                            Desactivate();
                        }
                    }
                }
            }

            for (int i = 0; i < Program.Enemies.Count; i++)
            {
                CheckCollision(Program.Enemies[i]);
            }
        }

        private void CheckCollision(Enemy enemy)
        {
            if (Collider.CheckCollision(collider, enemy.Collider))
            {

                enemy.TakeDamage();
                Desactivate();
            }
        }
    }
}
