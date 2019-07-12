using Game.Interfaces;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Player : GameObject, IRenderizable, IUpdateable
    {
        int move;

        float hp;
        float hpMax = 5;

        float xoffset;
        float yoffset;

        float xspd;
        float yspd;


        float speed = 150;
        float yspdMax = 30;

        int jumpForce = 30;

        int face = 1;

        float gravity = 1.5f;

        bool ground;
        bool isSpacePressed;
        bool jumping;


        private PoolBullets bulletsPool = new PoolBullets();
        private Gun activeGun;

        int bombs;
        bool isBombPress;

        bool isShoot;
        bool isReload;
        float timerShoot;
        float timeToShoot = 0.2f;

        Tilemap tilemap;
        Collider collider;

        Dictionary<StateMachine, Animation> animations = new Dictionary<StateMachine, Animation>();
        Animation currentAnimation;

        StateMachine currentState = StateMachine.idle_right;

        public Collider Collider { get => collider; set => collider = value; }
        public Tilemap Tilemap { get => tilemap; set => tilemap = value; }
        internal PoolBullets BulletsPool { get => bulletsPool; set => bulletsPool = value; }

        int factor = 10;

        private event SimpleEventHandler<Player> bombAction;

        public event SimpleEventHandler<Player> BombAction
        {
            add { bombAction += value; }
            remove { bombAction -= value; }
        }

        public Player(string initialSprite, Vector2D position, float angle, float xoffset, float yoffset) : base(position, angle)
        {
            this.xoffset = xoffset;
            this.yoffset = yoffset;

            collider = new Collider(position.X, position.Y, 32, 32, 16, 16, true, false);
            LoadAnimation();
            currentAnimation = animations[StateMachine.idle_right];
            activeGun = Program.weapons["pistol"];
            activeGun.Player = this;
            hp = hpMax;
            bombs = 3;
        }
        public void Render()
        {
            Engine.Draw(Sprite, Position, 1, 1, angle, 16, 16);

            // Lifebar
            Engine.Draw(@"img\sprites\lifeBarBG.png", new Vector2D(20, Program.ScreenHeight - 30), 1, 1, angle, 16, 16);
            Engine.Draw(@"img\sprites\lifeBar.png", new Vector2D(20, Program.ScreenHeight - 30), (hp / hpMax), 1, angle, 16, 16);

            new Text(activeGun.ToString(), 30, 30, 20, 26).drawText();
            new Text("bombs " + bombs.ToString(), Program.ScreenWidth - 200, Program.ScreenHeight - 15, 20, 26).drawText();
            new Text(activeGun.CurrentAmmo.ToString() + " / " + activeGun.MaxAmmo.ToString(), Program.ScreenWidth - 150, 30, 20, 26).drawText();


            activeGun.Render();
            activeGun.Face = face;
            currentAnimation.Animator();
            Sprite = currentAnimation.Sprite;
        }
        public void Update()
        {
            UpdateState();
            Movement();
            Weapon();

            currentAnimation = animations[currentState];

            collider.X = Position.X;
            collider.Y = Position.Y;
            activeGun.Update(position);
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

        private void Movement()
        {
            Gravity();
            CollisionTop();

            if (Engine.GetKey(Keys.A))
            {
                if (!isBombPress && bombs > 0)
                {
                    BombExplosion();
                    isBombPress = true;
                }
            }
            else
            {
                isBombPress = false;
            }

            if (position.Y - collider.OffsetY > 600)
            {
                position.X = 400;
                position.Y = 0;
            }


            if (TileID(position.X - collider.OffsetX + 4, position.Y + collider.OffsetY) != -1 || TileID(position.X + collider.OffsetX - 4, position.Y + collider.OffsetY) != -1)
            {
                ground = true;
                jumping = false;
            }
            else
            {
                ground = false;
            }

            if (!ground)
            {
                yspd = Utils.Focus(yspd, yspdMax, gravity);
            }

            if (Engine.GetKey(Keys.LSHIFT))
            {
                if (!isSpacePressed && ground)
                {
                    isSpacePressed = true;
                    jumping = true;
                    yspd -= jumpForce;
                }
            }
            else
            {
                isSpacePressed = false;
                if (!ground && jumping && yspd < 10)
                {
                    yspd = Utils.Focus(yspd, yspdMax, gravity * 2);
                }
            }

            if (Engine.GetKey(Keys.RIGHT))
            {
                move = 1;
                face = move;
                MoveRight();
            }
            else if (Engine.GetKey(Keys.LEFT))
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



            //Engine.Debug(jumping);
            //Engine.Debug(String.Format("xSpd: {0}, ySpd: {1}\nMove: {2}\nGround: {3}", xspd, yspd, move, ground));
            //Engine.Debug(String.Format("x: {0}, y: {1}", position.X, position.Y));
        }


        private void Weapon()
        {
            if (Engine.GetKey(Keys.X))
            {
                if (!isReload && !Engine.GetKey(Keys.Z))
                {
                    activeGun.Reload();
                    isReload = true;
                }
            }
            else
            {
                isReload = false;
            }

            if (Engine.GetKey(Keys.Z))
            {
                Shoot();
            }
            else
            {
                isShoot = false;
            }
            timerShoot += Program.DTime;
        }
        private void Shoot()
        {
            if (activeGun.CurrentAmmo > 0 && timerShoot >= timeToShoot)
            {
                if (activeGun.Automatic)
                {
                    activeGun.Shoot();
                }
                else
                {
                    if (!isShoot)
                    {
                        activeGun.Shoot();
                        isShoot = true;
                    }
                }
            }
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
                    xspd = 0;
                    break;
                }
            }
        }
        private void CollisionTop()
        {
            for (int i = 0; i < Math.Abs(yspd); i++)
            {

                if (TileID(position.X + collider.OffsetX - 5, position.Y - collider.OffsetY - 6) != -1 || TileID(position.X - collider.OffsetX + 5, position.Y - collider.OffsetY - 6) != -1)
                {
                    if (yspd < -2)
                    {
                        yspd = 0;
                        break;
                    }
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

        public void TakeDamage()
        {
            if (hp > 1)
            {
                hp--;
            }
            else
            {
                Program.ActualScreen = Screen.game_over;
                if (GameMananger.Score > GameMananger.HighScore)
                {
                    GameMananger.HighScore = GameMananger.Score;
                    SaveMananger.Instance.SaveCsv();
                }
            }
        }

        public void ChangeGun(IGun newGun)
        {
            activeGun = (Gun)newGun;
            activeGun.Player = this;
            activeGun.Reload();
        }
        public void BombExplosion()
        {
            bombs--;
            if (Program.Enemies.Count > 0)
            {
                bombAction(this);
            }
        }
    }
}