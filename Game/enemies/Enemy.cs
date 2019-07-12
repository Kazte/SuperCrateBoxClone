using System;
using System.Collections.Generic;

namespace Game
{
    public class Enemy : GameObject, IUpdateable, IRenderizable
    {

        public event SimpleEventHandler<Enemy> OnDeactivate;



        int hp;
        protected int initHP;

        protected Collider collider;

        protected Dictionary<StateMachine, Animation> animations = new Dictionary<StateMachine, Animation>();
        protected Animation currentAnimation;

        protected StateMachine currentState = StateMachine.idle_right;
        protected int face;
        protected int velocity;
        protected Tilemap tilemap;
        protected float yspd;
        protected float xspd;

        protected int factor = 10;
        protected bool ground;
        protected float gravity = 1.5f;
        protected float yspdMax = 30;

        protected int offsetX;
        protected int offsetY;
        
        Player player;

        public bool Destroyed { get; protected set; }
        public Collider Collider { get => collider; set => collider = value; }
        public int Hp { get => hp; set => hp = value; }

        public void Init(float x, float y, int face, int speed, float sizeX, float sizeY, Tilemap _tilemap, Player player)
        {
            position.X = x;
            position.Y = y;
            collider.SizeX = sizeX;
            collider.SizeY = sizeY;
            offsetX = (int) (sizeX / 2);
            offsetY = (int) (sizeY / 2);
            this.face = face;
            this.velocity = speed;
            this.tilemap = _tilemap;
            hp = initHP;
            this.player = player;
            currentAnimation = animations[StateMachine.walk_right];
            Destroyed = false;
            player.BombAction += new SimpleEventHandler<Player>(DestroyWithBomb);
            Program.Enemies.Add(this);
        }

        public Enemy(Vector2D position, float angle) : base(position, angle)
        {
            Collider = new Collider(position.X, position.Y, 20, 20, 10, 10, true, false);
            LoadAnimation();
            currentAnimation = animations[StateMachine.idle_right];
        }

        protected void DestroyWithBomb(Player player)
        {
            Desactivate();
        }

        protected virtual void LoadAnimation()
        {
            
        }

        public void TakeDamage()
        {
            if (hp > 0)
            {
                hp--;
            }
            else if (hp <= 0)
            {
                Desactivate();
            }
        }

        public void Render()
        {
            if (!Destroyed)
            {
                Engine.Draw(Sprite, Position, 1, 1, angle, offsetX, offsetY);
                currentAnimation.Animator();
                Sprite = currentAnimation.Sprite;
            }
        }

        public void Update()
        {
            UpdateState();
            collider.X = position.X;
            collider.Y = position.Y;
            currentAnimation = animations[currentState];

            if (player != null)
            {
                if (Collider.CheckCollision(player.Collider, collider))
                {
                    player.TakeDamage();
                    Desactivate();
                }
            }

            if (position.Y - collider.OffsetY > 600)
            {
                Desactivate();
            }

            Behaviour();
        }

        protected virtual void UpdateState()
        {
            
        }

        protected virtual void Behaviour()
        {
         
        }

        protected void MoveRight()
        {
            for (int i = 0; i < Math.Abs(xspd); i++)
            {
                if (TileID(position.X + collider.OffsetX, position.Y - collider.OffsetY + factor) == -1 && TileID(position.X + collider.OffsetX, position.Y + collider.OffsetY - factor) == -1)
                {
                    position.X += xspd;
                }
                else
                {
                    xspd = 0;
                    face *= -1;
                    break;
                }
            }
        }

        protected void MoveLeft()
        {
            for (int i = 0; i < Math.Abs(xspd); i++)
            {
                if (TileID(position.X - collider.OffsetX, position.Y - collider.OffsetY + factor) == -1 && TileID(position.X - collider.OffsetX, position.Y + collider.OffsetY - factor) == -1)
                {
                    position.X += xspd;
                }
                else
                {
                    face *= -1;
                    xspd = 0;
                    break;
                }
            }
        }


        protected void Gravity()
        {
            for (int i = 0; i < Math.Abs(yspd); i++)
            {
                if (yspd > 0)
                {
                    if (TileID(position.X - collider.OffsetX + 4, position.Y + collider.OffsetY) != -1 || TileID(position.X + collider.OffsetX - 4, position.Y + collider.OffsetY) != -1)
                    {
                        yspd = 0;
                        break;
                    }
                }
                position.Y += yspd * Program.DTime;
            }

        }


        protected int TileID(float x, float y)
        {
            int row = (int)(y / 32);
            int col = (int)(x / 32);
            if (col > tilemap.Tiledata.GetLength(1) - 1) { col = tilemap.Tiledata.GetLength(1) - 1; }
            if (col < 0) { col = 0; }
            if (row > tilemap.Tiledata.GetLength(0) - 1) { row = tilemap.Tiledata.GetLength(0) - 1; }
            if (row < 0) { row = 0; }
            return tilemap.Tiledata[row, col];
        }

        public void Desactivate()
        {
            Destroyed = true;
            Program.Enemies.Remove(this);

            if (OnDeactivate != null)
            {
                OnDeactivate.Invoke(this);
            }
        }
    }
}
