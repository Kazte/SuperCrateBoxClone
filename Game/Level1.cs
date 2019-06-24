using System.Collections.Generic;
using System.IO;

namespace Game
{
    public class Level1 : IUpdateable
    {

        Tileset tileset;
        Tilemap tilemap;

        Player player;

        EnemyGenerator generator;
        CrateGenerator crates;

        public Level1()
        {
            ResetLevel(25, 19);
        }

        public void ResetLevel(int tilemapRow, int tilemapCol)
        {
            GameMananger.Score = 0;

            for (int i = 0; i < Program.Enemies.Count; i++)
            {
                Program.Enemies[i].Desactivate();
            }
            for (int i = 0; i < Program.Crates.Count; i++)
            {
                Program.Crates[i].Desactivate();
            }

            tileset = new Tileset(LoadTileset(11, "img/tileset/Level1/tileset_lvl1_"));
            tilemap = new Tilemap(tilemapCol, tilemapRow, 32, tileset, "img/tileset/background_lvl1.png");
            SetMap();
            player = PlayerFactory.CreatePlayer(new Vector2D(400, 200), tilemap);
            generator = new EnemyGenerator(tilemap, player);
            crates = new CrateGenerator(tilemap, player);
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

            string a = GameMananger.Score < 10 ? (a = "0" + GameMananger.Score) : GameMananger.Score.ToString();
            new Text(a, Program.ScreenWidth / 2 - 20, Program.ScreenHeight / 2).drawText();


            player.Render();

            for (int i = 0; i < Program.Enemies.Count; i++)
            {
                Program.Enemies[i].Render();
            }
            for (int i = 0; i < Program.Bullets.Count; i++)
            {
                Program.Bullets[i].Render();
            }
            for (int i = 0; i < Program.Crates.Count; i++)
            {
                Program.Crates[i].Render();
            }
        }

        public void Update()
        {
            tileset.Update();
            player.Update();
            generator.Update();
            crates.Update();
            for (int i = 0; i < Program.Enemies.Count; i++)
            {
                Program.Enemies[i].Update();
            }
            for (int i = 0; i < Program.Bullets.Count; i++)
            {
                Program.Bullets[i].Update();
            }
            for (int i = 0; i < Program.Crates.Count; i++)
            {
                Program.Crates[i].Update();
            }
        }

    }
}
