using System.Collections.Generic;

namespace Game
{
    class PoolEnemies
    {
        private List<Enemy> inUse = new List<Enemy>();
        private List<Enemy> available = new List<Enemy>();

        public Enemy Get()
        {
            Enemy enemy;
            if (available.Count > 0)
            {
                enemy = available[0];
                available.Remove(enemy);
            }
            else
            {
                enemy = new Enemy(new Vector2D(0, 0), 0);
                enemy.OnDeactivate += OnBulletDeactivateHandler;
            }

            inUse.Add(enemy);
            return enemy;
        }

        private void OnBulletDeactivateHandler(Enemy enemy)
        {
            Release(enemy);
        }

        public void Release(Enemy enemy)
        {
            inUse.Remove(enemy);
            available.Add(enemy);
        }
    }
}
