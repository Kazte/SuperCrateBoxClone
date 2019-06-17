﻿using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class WeaponFactory
    {
        public static IGun PickWeapon()
        {
            return Program.weapons["pistol"];
        }
    }
}