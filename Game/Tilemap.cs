namespace Game
{
    public class Tilemap : IUpdateable
    {
        int row;
        int col;
        int size;
        int[,] tiledata;
        Tile[,] tiles;
        Tileset tileset;

        string spriteBG;

        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
        public int[,] Tiledata { get => tiledata; set => tiledata = value; }

        public Tilemap(int row, int col, int tilesize, Tileset tileset, string spriteBG)
        {
            this.row = row;
            this.col = col;
            this.size = tilesize;
            this.tileset = tileset;
            this.spriteBG = spriteBG;
            tiledata = new int[row, col];
            tiles = new Tile[row, col];
        }

        public void SetTile(int x, int y, int tile)
        {
            tiledata[x, y] = tile;
        }

        public void SetTileset(Tileset tileset)
        {
            this.tileset = tileset;
        }
        public Tile GetTile(int i, int j)
        {
            return tiles[i, j];
        }

        public void Initialize()
        {
            for (int row = 0; row < this.row; row++)
            {
                for (int col = 0; col < this.col; col++)
                {
                    int x = col * 32;
                    int y = row * 32;
                    if (tiledata[row, col] != -1)
                    {
                        tiles[row, col] = new Tile(tileset.GetTile(tiledata[row, col]).Sprite, new Vector2D(x + 16, y + 16));
                    }
                }
            }
        }

        public void Render()
        {
            // Draw Background
            Engine.Draw(spriteBG, 0, 0);

            // Draw tiles
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    if (tiles[x, y] != null)
                    {
                        tiles[x, y].Render();
                    }
                }
            }
        }

        public void Update()
        {
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    if (tiles[x, y] != null)
                    {
                        tiles[x, y].Update();
                    }
                }
            }
        }
    }
}


