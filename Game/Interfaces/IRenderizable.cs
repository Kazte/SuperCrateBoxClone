﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IRenderizable
    {
        string Sprite { get; set; }

        void Render();
    }
}
