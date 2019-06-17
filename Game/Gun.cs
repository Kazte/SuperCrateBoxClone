using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Reload()
        {
            CurrentAmmo = MaxAmmo;
            Engine.Debug(ToString() + "Reload");
        }

        public virtual void Render()
        {
            
        }

        public void Shoot()
        {
            currentAmmo--;

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
