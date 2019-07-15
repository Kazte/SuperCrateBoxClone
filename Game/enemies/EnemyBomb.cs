namespace Game
{
    public class EnemyBomb : Enemy
    {
        public EnemyBomb(Vector2D position, float angle, Tilemap tilemap, Player player) : base(position, angle, tilemap, player)
        {
            hp = 3;
            sizeX = 38;
            sizeY = 50;
            offsetX = 38/2;
            offsetY = 50/2;
            collider = new Collider(position.X, position.Y, sizeX, sizeY, true, false);
            velocity = 130;
        }
        
        protected override void LoadAnimation()
        {
            animations.Add(StateMachine.walk_right, new Animation("img/sprites/enemy_bomb/walk_right", true));
            animations.Add(StateMachine.walk_left, new Animation("img/sprites/enemy_bomb/walk_left", true));
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