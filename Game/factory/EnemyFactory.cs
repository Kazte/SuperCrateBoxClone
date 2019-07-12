namespace Game
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(Vector2D position)
        {
            int choose = Program.random.Next(0);

            switch (choose)
            {
                case 0:
                    return new EnemyTiny(position,0f);
                
                default:
                    Engine.Debug("Error at EnemyFactory");
                    return null;
            }
        }
    }
}