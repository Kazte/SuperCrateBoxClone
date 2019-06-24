using Game.Interfaces;

namespace Game
{
    public class Gun : GameObject, IRenderizable, IGun
    {
        public Gun(Vector2D position, float angle, int maxAmmo, bool automatic, int bulletSpeed) : base(position, angle)
        {
            this.MaxAmmo = maxAmmo;
            CurrentAmmo = maxAmmo;
            this.Automatic = automatic;
            this.bulletSpeed = bulletSpeed;

        }
        Player player;
        Bullet bullet;

        int maxAmmo;
        int currentAmmo;
        bool automatic;

        int bulletSpeed;



        int face;

        public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
        public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
        public bool Automatic { get => automatic; set => automatic = value; }
        public int Face { get => face; set => face = value; }
        public int BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
        public Bullet Bullet { get => bullet; set => bullet = value; }
        public Player Player { get => player; set => player = value; }

        public void Reload()
        {
            CurrentAmmo = MaxAmmo;
        }

        public virtual void Render()
        {

        }

        public virtual void Shoot()
        {


        }

        public virtual void Update(Vector2D position)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
