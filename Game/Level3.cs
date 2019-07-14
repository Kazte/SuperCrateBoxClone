using System.Collections.Generic;
using System.IO;

namespace Game
{
    public class Level3 : IUpdateable
    {
        Tileset tileset;
        Tilemap tilemap;

        Player player;
        

        EnemyGenerator generator;
        CrateGenerator crates;

        public Level3()
        {
            ResetLevel(25, 19);
        }

        public void ResetLevel(int tilemapRow, int tilemapCol)
        {
            GameMananger.Score = 0;

            for (var index = 0; index < Program.Enemies.Count; index++)
            {
                var enemy = Program.Enemies[index];
                enemy.Destroy();
            }


            for (var index = 0; index < Program.Crates.Count; index++)
            {
                var crate = Program.Crates[index];
                crate.Desactivate();
            }

            tileset = new Tileset(LoadTileset(12, "img/tileset/Level3/tileset_lvl2_"));
            tilemap = new Tilemap(tilemapCol, tilemapRow, 32, tileset, "img/tileset/background_lvl3.png");
            SetMap("maps/Lvl3.csv");
            player = PlayerFactory.CreatePlayer(new Vector2D(400, 200), tilemap);
            generator = new EnemyGenerator(tilemap, player);
            crates = new CrateGenerator(tilemap, player);
        }

        private void SetMap(string mapPath)
        {
            StreamReader sr = new StreamReader(mapPath);
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

            for (var index = 0; index < Program.Enemies.Count; index++)
            {
                var enemy = Program.Enemies[index];
                enemy.Render();
            }


            for (var index = 0; index < Program.Bullets.Count; index++)
            {
                var bullet = Program.Bullets[index];
                bullet.Render();
            }

            for (var index = 0; index < Program.Crates.Count; index++)
            {
                var crate = Program.Crates[index];
                crate.Render();
            }
        }

        public void Update()
        {
            tileset.Update();
            player.Update();
            generator.Update();
            crates.Update();
            for (var i = 0; i < Program.Enemies.Count; i++)
            {
                var enemy = Program.Enemies[i];
                enemy.Update();
            }

            for (var i = 0; i < Program.Bullets.Count; i++)
            {
                var bullet = Program.Bullets[i];
                bullet.Update();
            }

            for (var i = 0; i < Program.Crates.Count; i++)
            {
                var crate = Program.Crates[i];
                crate.Update();
            }
        }
    }
}