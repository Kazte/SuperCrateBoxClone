using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Level1 : IUpdateable
    {

        Tileset tileset;
        Tilemap tilemap;

        Player player;

        public Level1(int tilemapRow, int tilemapCol)
        {
            tileset = new Tileset(LoadTileset(11, "img/tileset/Level1/tileset_lvl1_"));
            tilemap = new Tilemap(tilemapCol, tilemapRow, 32, tileset, "img/tileset/background_lvl1.png");
            SetMap();
            player = PlayerFactory.CreatePlayer(new Vector2D(400, 5), tilemap);
        }

        private void SetMap()
        {
            StreamReader sr = new StreamReader("maps/Lvl1.csv");
            string strResult = sr.ReadToEnd();
            string[] arrayResult = strResult.Split(',');
            int tileid;
            int count = 0;
            for (int x = 0; x < tilemap.Row; x++)
            {
                for (int y = 0; y < tilemap.Col; y++)
                {
                    tileid = int.Parse(arrayResult[count]);
                    tilemap.SetTile(x, y, tileid);
                    count++;
                }
            }
            tilemap.Initialize();
        }

        private static List<Tile> LoadTileset(int tilesetSize, string initialPath)
        {
            List<Tile> tilesaux = new List<Tile>();
            for (int i = 0; i <= tilesetSize; i++)
            {
                string path = initialPath + i.ToString() + ".png";
                tilesaux.Add(new Tile(path, new Vector2D(0, 0), i));
                Engine.Debug("Load texture: " + path);
            }
            return tilesaux;
        }

        public void Render()
        {
            tilemap.Render();
            player.Render();
        }

        public void Update()
        {
            tileset.Update();
            player.Update();
        }

    }
}
