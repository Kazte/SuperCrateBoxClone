using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.weapons
{
    class AK : Gun
    {
        public AK(Vector2D position, float angle, int maxAmmo, bool automatic, int bulletSpeed) : base(position, angle, maxAmmo, automatic, bulletSpeed)
        {

        }
        public override void Shoot()
        {
            Random random = new Random();

            CurrentAmmo--;
            if (Face == 1)
            {
                Bullet.Init(position.X, position.Y, Face, BulletSpeed, random.Next(-10, 10));
            }
            else
            {
                Bullet.Init(position.X, position.Y, Face, BulletSpeed, random.Next(170, 190));
            }
        }

        public override void Render()
        {
            if (Face == 1)
            {
                Engine.Draw(@"img\weapons\weapon_4.png", new Vector2D(position.X + 10, position.Y + 5), 1, 1, 0, 16, 16);
            }
            else
            {
                Engine.Draw(@"img\weapons\weapon_-4.png", new Vector2D(position.X - 10, position.Y + 5), 1, 1, 0, 16, 16);
            }
        }
    }
}
