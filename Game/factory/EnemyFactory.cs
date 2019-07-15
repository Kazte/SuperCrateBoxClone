namespace Game
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(Vector2D position, Tilemap tilemap, Player player)
        {
            int choose = Program.random.Next(0, 3);

            switch (choose)
            {
                case 0:
                    return new EnemyTiny(position, 0f, tilemap, player);
                case 1:
                    return new EnemyBomb(position, 0f, tilemap, player);
                case 2:
                    return new EnemyRed(position, 0f, tilemap, player);
                
                default:
                    Engine.Debug("Error at EnemyFactory");
                    return null;
            }
        }
    }
}