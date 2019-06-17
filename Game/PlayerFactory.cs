using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class PlayerFactory
    {
        public static Player CreatePlayer(Vector2D position, Tilemap tilemap)
        {
            Player player = new Player("img/sprites/player/player_idle1.png", position, 0, 32, 32);
            player.Tilemap = tilemap;
            return player;
        }
    }
}
