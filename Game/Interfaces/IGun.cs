using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Interfaces
{
    public interface IGun
    {
        int MaxAmmo { get; set; }
        int CurrentAmmo { get; set; }
        bool Automatic { get; set; }
        void Shoot();
        void Reload();
    }
}
