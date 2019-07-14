using System;

namespace Game
{
    public class EnemyGenerator : IUpdateable
    {
        Player player;

        Tilemap tilemap;

        float timer = 5;

        bool diffChange;

        float min = 1f;
        float max = 6f;

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
                timer = (float)Math.Floor(Program.random.NextDouble() * (max - min + 1) + min);
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
            var enemy = EnemyFactory.CreateEnemy(new Vector2D(400, 0), tilemap, player);
            Program.Enemies.Add(enemy);
        }
    }
}
