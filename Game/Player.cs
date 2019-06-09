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
        float move;

        float xspdMax = 700;
        float yspdMax = 650;

        float accGround = 70;
        float fricGround = 500;
        float accAir = 200;
        float fricAir = 500;

        int jumpForce = 800;

        int face = 1;

        int gravity = 50;

        bool ground;
        bool isSpacePressed;

        float accTemp;
        float fricTemp;

        private IGun activeGun;


        Collider collider;
        Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        public Collider Collider { get => collider; set => collider = value; }

        public Player(string initialSprite, Vector2D position, float angle, float xoffset, float yoffset) : base(position, angle)
        {
            this.xoffset = xoffset;
            this.yoffset = yoffset;

            collider =  new Collider(position.X, position.Y, 32, 32, xoffset, yoffset, true, false);

            sprite = initialSprite;

        }

       

        void Movement()
        {

            ground = position.Y + 16 >= 544 ? true : false;

            accTemp = ground ? accGround : accAir;
            fricTemp = ground ? fricGround : fricAir;

            // Gravity
            if (!ground)
            {
                yspd = Utils.Focus(yspd, yspdMax, gravity);
            }
            else
            {
                yspd = 0;
            }

            if (Engine.GetKey(Keys.SPACE))
            {
                if (!isSpacePressed && ground)
                {
                    isSpacePressed = true;
                    Jump();
                }
            }
            else
            {
                isSpacePressed = false;
                if (!ground)
                {
                    yspd = Utils.Focus(yspd, yspdMax, gravity * .8f); 
                }
            }


            if (Engine.GetKey(Keys.RIGHT))
            {
                move = 1;
                face = 1;
            }
            else if(Engine.GetKey(Keys.LEFT))
            {
                move = -1;
                face = -1;
            }
            else
            {
                move = 0;
            }

            //xspd = move != 0 ? Utils.Focus(xspd, xspdMax * move, accTemp) : Utils.Focus(xspd, xspdMax * move, fricTemp);
            xspd = move * 500;

            position.X += xspd * Program.DTime;
            position.Y += yspd * Program.DTime;

            Engine.Debug(String.Format("xSpd: {0}, ySpd: {1}\nMove: {2}", xspd, yspd, move));
        }

        private void Jump()
        {
            yspd -= jumpForce;
        }


        void ChangeGun(IGun newGun)
        {
            activeGun = newGun;
        }



        public void Render()
        {
            Engine.Draw(Sprite, Position, 1, 1, angle, 16, 16);
            //collider.DrawCollider();
        }

        public void Update()
        {
            Movement();
            collider.X = Position.X;
            collider.Y = Position.Y;
        }
    }
}