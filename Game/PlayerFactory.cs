using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class PlayerFactory
    {
        public static Player CreatePlayer(Vector2D position)
        {
            return new Player("img/sprites/player/player_idle1.png", position, 0, 16, 16);
        }
    }
}
