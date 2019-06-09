using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Tileset : IUpdateable
    {

        private List<Tile> tiles;

        public Tileset(List<Tile> tiles)
        {
            this.tiles = tiles;
        }

        public Tile GetTile(int id)
        {
            return tiles.ElementAt(id);
        }

        public void Render()
        {
            foreach (Tile tile in tiles)
            {
                tile.Render();
            }
        }

        public void Update()
        {
            foreach (Tile tile in tiles)
            {
                tile.Update();
            }
        }
    }
}
