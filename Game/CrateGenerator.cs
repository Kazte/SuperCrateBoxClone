using System;
using System.Collections.Generic;

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

        List<Vector2D> createPositions;

        // TODO: Se generen en lugares random

        public CrateGenerator(Tilemap tilemap, Player player)
        {
            this.tilemap = tilemap;
            this.player = player;

            createPositions = new List<Vector2D>();
            createPositions.Add(new Vector2D(690, 80));
            createPositions.Add(new Vector2D(115, 80));
            createPositions.Add(new Vector2D(400, 240));
            createPositions.Add(new Vector2D(260, 336));
            createPositions.Add(new Vector2D(545, 336));

        }

        public void Update()
        {
            timer -= Program.DTime;
            if (timer < 0 && cratePool.InUseCount() < 2)
            {
                timer = (float)Math.Floor(random.NextDouble() * (max - min + 1) + min);
                GenerateCrate();
            }
        }

        private void GenerateCrate()
        {
            var crate = cratePool.Get();
            Vector2D position = createPositions[random.Next(createPositions.Count)];
            crate.Init(position.X, position.Y, tilemap, player);
        }
    }
}
