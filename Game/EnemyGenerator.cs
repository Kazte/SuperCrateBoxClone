using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyGenerator : IUpdateable
    {
        private PoolEnemies enemiesPool = new PoolEnemies();
        Player player;

        Tilemap tilemap;

        float timer = 5;



        float min = 2f;
        float max = 6f;
        Random random = new Random();
        // TODO: Hacer generador de enemigos cada más tiempo que pase más rápido salen.

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
        }

        private void GenerateEnemy()
        {
            var enemy = enemiesPool.Get();

            if (random.NextDouble() > 0.5f) {
                enemy.Init(400, 0, 1, random.Next(100, 120), tilemap, player);
            }
            else
            {
                enemy.Init(400, 0, -1, random.Next(100, 150), tilemap, player);
            }
        }
    }
}
