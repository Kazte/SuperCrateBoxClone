using System;

namespace Game
{
    public class EnemyGenerator : IUpdateable
    {
        private PoolEnemies enemiesPool = new PoolEnemies();
        Player player;

        Tilemap tilemap;

        float timer = 5;

        bool diffChange;

        float min = 1f;
        float max = 6f;
        Random random = new Random();

        public EnemyGenerator(Tilemap tilemap, Player player)
        {
            this.tilemap = tilemap;
            this.player = player;
        }

        public void Update()
        {
            timer -= Program.DTime;
            if (timer < 0)
            {
                timer = (float)Math.Floor(random.NextDouble() * (max - min + 1) + min);
                GenerateEnemy();
            }

            if (GameMananger.Score % 5 == 0)
            {
                if (!diffChange)
                {
                    if (max > 1)
                    {
                        max--;
                        diffChange = true;
                    }
                }
            }
            else
            {
                diffChange = false;
            }
        }

        private void GenerateEnemy()
        {
            var enemy = enemiesPool.Get();

            if (random.NextDouble() > 0.5f)
            {
                enemy.Init(400, 0, 1, random.Next(100, 120), 20, 20, tilemap, player);
            }
            else
            {
                enemy.Init(400, 0, -1, random.Next(100, 120), 20, 20, tilemap, player);
            }
        }
    }
}
