using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CrateGenerator
    {
        private PoolCrate cratePool = new PoolCrate();
        Player player;

        Tilemap tilemap;

        float timer = 5;

        float min = 2f;
        float max = 6f;
        Random random = new Random();

        // TODO: Se generen en lugares random

        public CrateGenerator(Tilemap tilemap, Player player)
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
                GenerateCrate();
            }
        }

        private void GenerateCrate()
        {
            var crate = cratePool.Get();
            crate.Init(400, 0, tilemap, player);
        }
    }
}
