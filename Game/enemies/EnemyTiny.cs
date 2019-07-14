using System.Diagnostics;

namespace Game
{
    public class EnemyTiny : Enemy
    {
        public EnemyTiny(Vector2D position, float angle, Tilemap tilemap, Player player) : base(position, angle, tilemap, player)
        {
            hp = 5;
            sizeX = 20;
            sizeY = 20;
            offsetX = 20;
            offsetY = 20;
            Collider = new Collider(position.X, position.Y, sizeX, sizeY, true, false);
            velocity = Program.random.Next(100, 120);

        }

        protected override void LoadAnimation()
        {
            animations.Add(StateMachine.walk_right, new Animation("img/sprites/enemy_tiny/walk_right", true));
            animations.Add(StateMachine.walk_left, new Animation("img/sprites/enemy_tiny/walk_left", true));
        }

        protected override void Behaviour()
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

        protected override void UpdateState()
        {
            
            currentState = face == 1 ? StateMachine.walk_right : StateMachine.walk_left;
        }
    }
}