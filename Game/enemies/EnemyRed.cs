namespace Game
{
    public class EnemyRed : Enemy
    {
        public EnemyRed(Vector2D position, float angle, Tilemap tilemap, Player player) : base(position, angle, tilemap,
            player)
        {
            hp = 10;
            sizeX = 40;
            sizeY = 34;
            offsetX = 40/2;
            offsetY = 34/2;
            collider = new Collider(position.X, position.Y, sizeX, sizeY, true, false);
            velocity = 70;
        }

        protected override void LoadAnimation()
        {
            animations.Add(StateMachine.walk_right, new Animation("img/sprites/enemy_red/walk_right", true));
            animations.Add(StateMachine.walk_left, new Animation("img/sprites/enemy_red/walk_left", true));
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