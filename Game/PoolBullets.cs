using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class PoolBullets
    {
        private List<Bullet> inUse = new List<Bullet>();
        private List<Bullet> available = new List<Bullet>();

        public Bullet Get()
        {
            Bullet bullet;
            if (available.Count > 0)
            {
                bullet = available[0];
                available.Remove(bullet);
            }
            else
            {
                bullet = new Bullet(new Vector2D(0, 0), 0);
                bullet.OnDeactivate += OnBulletDeactivateHandler;
            }

            inUse.Add(bullet);
            return bullet;
        }

        private void OnBulletDeactivateHandler(Bullet bullet)
        {
            Release(bullet);
        }

        public void Release(Bullet bullet)
        {
            inUse.Remove(bullet);
            available.Add(bullet);
        }
    }
}
