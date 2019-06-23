using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Enemy : GameObject, IUpdateable, IRenderizable
    {

        public event SimpleEventHandler<Enemy> OnDeactivate;
        


        int hp;
        int initHP = 5;

        Collider collider;

        Dictionary<StateMachine, Animation> animations = new Dictionary<StateMachine, Animation>();
        Animation currentAnimation;

        StateMachine currentState = StateMachine.idle_right;
        private int face;
        private int velocity;
        private Tilemap tilemap;
        private float yspd;
        private float xspd;

        int factor = 10;
        private bool ground;
        private float gravity = 1.5f;
        private float yspdMax = 30;

        Player player;

        public bool Destroyed { get; private set; }
        public Collider Collider { get => collider; set => collider = value; }
        public int Hp { get => hp; set => hp = value; }

        public void Init(float x, float y, int face, int speed, Tilemap tilemap, Player player)
        {
            position.X = x;
            position.Y = y;
            this.face = face;
            this.velocity = speed;
            this.tilemap = tilemap;
            hp = initHP;
            this.player = player;
            currentAnimation = animations[StateMachine.idle_right];
            Destroyed = false;
            Program.Enemies.Add(this);
        }

        public Enemy(Vector2D position, float angle) : base(position, angle)
        {
            Collider = new Collider(position.X, position.Y, 20, 20, 10, 10, true, false);
            LoadAnimation();
            currentAnimation = animations[StateMachine.idle_right];
            
        }

        private void LoadAnimation()
        {
            animations.Add(StateMachine.idle_right, new Animation("img/sprites/enemy_tiny/idle_right", true));
            animations.Add(StateMachine.idle_left, new Animation("img/sprites/enemy_tiny/idle_left", true));
            animations.Add(StateMachine.walk_right, new Animation("img/sprites/enemy_tiny/walk_right", true));
            animations.Add(StateMachine.walk_left, new Animation("img/sprites/enemy_tiny/walk_left", true));
            animations.Add(StateMachine.death_right, new Animation("img/sprites/enemy_tiny/death_right", true));
            animations.Add(StateMachine.death_left, new Animation("img/sprites/enemy_tiny/death_left", true));

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
                Engine.Draw(Sprite, Position, 1, 1, angle, 16, 16);

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
                    Program.ActualScreen = Screen.game_over;
                }
            }

            if (position.Y - collider.OffsetY > 600)
            {
                Desactivate();
            }

            Movement();
        }

        private void UpdateState()
        {
            if (face == 1)
            {
                currentState = StateMachine.walk_right;
            }
            else
            {
                currentState = StateMachine.walk_left;
            }
        }

        private void Movement()
        {
            if (TileID(position.X - collider.OffsetX + 4, position.Y + collider.OffsetY) != -1 || TileID(position.X + collider.OffsetX - 4, position.Y + collider.OffsetY) != -1)
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

            if (face == 1)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }

            xspd = face * Program.DTime * velocity;

            Gravity();
        }

        private void MoveRight()
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

        private void MoveLeft()
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


        private void Gravity()
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
