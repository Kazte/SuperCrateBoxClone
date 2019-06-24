namespace Game
{
    public class Pistol : Gun
    {
        public Pistol(Vector2D position, float angle, int maxAmmo, bool automatic, int bulletSpeed) : base(position, angle, maxAmmo, automatic, bulletSpeed)
        {

        }

        public override void Shoot()
        {
            CurrentAmmo--;
            if (Face == 1)
            {
                var bullet = Player.BulletsPool.Get();
                bullet.Tilemap = Player.Tilemap;
                bullet.Init(position.X, position.Y, Face, BulletSpeed, 0);
            }
            else
            {
                var bullet = Player.BulletsPool.Get();
                bullet.Tilemap = Player.Tilemap;
                bullet.Init(position.X, position.Y, Face, BulletSpeed, 180);
            }


        }

        public override void Render()
        {
            if (Face == 1)
            {
                Engine.Draw(@"img\weapons\weapon_1.png", new Vector2D(position.X + 10, position.Y + 5), 1, 1, 0, 16, 16);
            }
            else
            {
                Engine.Draw(@"img\weapons\weapon_-1.png", new Vector2D(position.X - 10, position.Y + 5), 1, 1, 0, 16, 16);
            }
        }
    }
}
