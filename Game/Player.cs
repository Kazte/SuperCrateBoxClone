using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Interfaces;

namespace Game
{
    public class Player : GameObject, IRenderizable, IUpdateable
    {
        float xoffset;
        float yoffset;

        float xspd;
        float yspd;
        int move;
        float speed = 150;

        float xspdMax = 700;
        float yspdMax = 90;

        float accGround = 70;
        float fricGround = 500;
        float accAir = 0.2f;
        float fricAir = 0.5f;

        int jumpForce = 800000;

        int face = 1;

        float gravity = 1.5f;

        bool ground;
        bool isSpacePressed;

        float accTemp;
        float fricTemp;

        private IGun activeGun;

        Tilemap tilemap;

        Collider collider;

        Dictionary<StateMachine, Animation> animations = new Dictionary<StateMachine, Animation>();
        Animation currentAnimation;

        StateMachine currentState = StateMachine.idle_right;

        public Collider Collider { get => collider; set => collider = value; }
        public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

        int factor = 5;

        public Player(string initialSprite, Vector2D position, float angle, float xoffset, float yoffset) : base(position, angle)
        {
            this.xoffset = xoffset;
            this.yoffset = yoffset;

            collider =  new Collider(position.X, position.Y, 16, 16, xoffset, yoffset, true, false);
            LoadAnimation();
            currentAnimation = animations[StateMachine.idle_right];

        }

        private void LoadAnimation()
        {
            animations.Add(StateMachine.idle_right, new Animation("img/sprites/player/idle_right", true));
            animations.Add(StateMachine.idle_left, new Animation("img/sprites/player/idle_left", true));
            animations.Add(StateMachine.walk_right, new Animation("img/sprites/player/walk_right", true));
            animations.Add(StateMachine.walk_left, new Animation("img/sprites/player/walk_left", true));
            animations.Add(StateMachine.jump_right, new Animation("img/sprites/player/jump_right", true));
            animations.Add(StateMachine.jump_left, new Animation("img/sprites/player/jump_left", true));

        }

        void Movement()
        {

            

            if (TileID(position.X + factor, position.Y + collider.SizeY) != -1 || TileID(position.X + collider.SizeX - factor, position.Y + collider.SizeY) != -1)
            {
                ground = true;
            }
            else
            {
                ground = false;
            }

            accTemp = ground ? accGround : accAir;
            fricTemp = ground ? fricGround : fricAir;


            if (!ground)
            {
                yspd = Utils.Focus(yspd, yspdMax, gravity);
            }


            if (Engine.GetKey(Keys.SPACE))
            {
                if (!isSpacePressed && ground)
                {
                    isSpacePressed = true;
                    yspd -= jumpForce;
                    Engine.Debug("sd");
                }
            }
            else
            {
                isSpacePressed = false;
                //if (!ground)
                //{
                //    yspd = Utils.Focus(yspd, yspdMax, gravity * .8f);
                //}
            }

            if (Engine.GetKey(Keys.RIGHT))
            {
                move = 1;
                face = move;
                MoveRight();
            }
            else if(Engine.GetKey(Keys.LEFT))
            {
                move = -1;
                face = move;
                MoveLeft();
            }
            else
            {
                move = 0;
            }

            xspd = move * Program.DTime * speed;

            Gravity();
            CollisionTop();
            //Engine.Debug(String.Format("xSpd: {0}, ySpd: {1}\nMove: {2}\nGround: {3}", xspd, yspd, move, ground));
        }


        void ChangeGun(IGun newGun)
        {
            activeGun = newGun;
        }



        public void Render()
        {
            Engine.Draw(Sprite, Position, 1, 1, angle, 16, 16);
            currentAnimation.Animator();
            Sprite = currentAnimation.Sprite;
        }

        public void Update()
        {
            UpdateState();
            Movement();
            currentAnimation = animations[currentState];

            collider.X = Position.X;
            collider.Y = Position.Y;
        }

        private void MoveRight()
        {
            for (int i = 0; i < Math.Abs(xspd); i++) { 
                if (TileID(position.X + collider.SizeX, position.Y + factor) == -1 && TileID(position.X + collider.SizeX, position.Y - factor) == -1)
                {
                    position.X += xspd;
                }
                else
                {
                    xspd = 0;
                    break;
                }
            }
        }

        private void MoveLeft()
        {
            for (int i = 0; i < Math.Abs(xspd); i++)
            {
                if (TileID(position.X, position.Y + factor) == -1 && TileID(position.X, position.Y + collider.SizeY - factor) == -1)
                {
                    position.X += xspd;
                }
                else
                {
                    xspd = 0;
                    break;
                }
            }
        }

        private void CollisionTop()
        {
            for (int i = 0; i < Math.Abs(yspd); i++)
            {
                if (TileID(position.X + factor, position.Y) != -1 || TileID(position.X + collider.SizeX - factor, position.Y) != -1)
                {
                    yspd = 0;
                    break;
                }
            }
        }

        private void Gravity()
        {
            for (int i = 0; i < Math.Abs(yspd); i++)
            {
                if (TileID(position.X + factor, position.Y + collider.SizeY) != -1 || TileID(position.X + collider.SizeX - factor, position.Y + collider.SizeY) != -1)
                {
                    yspd = 0;
                    break;
                }
                else
                {
                    position.Y += yspd * Program.DTime;
                }
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

        private void UpdateState()
        {
            if (ground)
            {
                if (move == 0)
                {
                    if (face == 1)
                    {
                        currentState = StateMachine.idle_right;
                    }
                    else
                    {
                        currentState = StateMachine.idle_left;
                    }
                }
                else if (move == 1)
                {
                    currentState = StateMachine.walk_right;
                }
                else
                {
                    currentState = StateMachine.walk_left;
                }
            }
            else
            {
                if (move == 1)
                {
                    currentState = StateMachine.jump_right;
                }
                else
                {
                    currentState = StateMachine.jump_left;
                }
            }
        }
    }
}