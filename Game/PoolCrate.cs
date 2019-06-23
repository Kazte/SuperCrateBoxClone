using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PoolCrate
    {
        private List<Crate> inUse = new List<Crate>();
        private List<Crate> available = new List<Crate>();

        public Crate Get()
        {
            Crate crate;
            if (available.Count > 0)
            {
                crate = available[0];
                available.Remove(crate);
                Engine.Debug("Take crate");
            }
            else
            {
                crate = new Crate(new Vector2D(0, 0), 0);
                crate.OnDeactivate += OnBulletDeactivateHandler;
                Engine.Debug("Create crate");
            }

            inUse.Add(crate);
            return crate;
        }

        private void OnBulletDeactivateHandler(Crate crate)
        {
            Release(crate);
        }

        public void Release(Crate crate)
        {
            inUse.Remove(crate);
            available.Add(crate);
        }
    }
}
