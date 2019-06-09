using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game
{
    public class Animation
    {
        private List<Texture> sprites;
        private float speed = 0.05f;
        private bool isLoop;
        private int indexAnim;
        private float animTimer;
        private Texture sprite;

        public List<Texture> Sprites { get => sprites; set => sprites = value; }
        public float Speed { get => speed; set => speed = value; }
        public bool IsLoop { get => isLoop; set => isLoop = value; }
        public float AnimTimer { get => animTimer; set => animTimer = value; }

        public Animation(string path)
        {
            LoadTextures(path);
        }


        void LoadTextures(string path)
        {
            DirectoryInfo myDir = new DirectoryInfo(path);
            if (myDir.GetFiles().Length > 1)
            {
                foreach (FileInfo file in myDir.GetFiles())
                {
                    sprites.Add(Engine.GetTexture(file.FullName));
                }
            }
            else
            {
                sprites.Add(Engine.GetTexture(myDir.GetFiles()[0].FullName));
            }
        }

        public void Animator()
        {
            if (AnimTimer > Speed)
            {
                if (IsLoop)
                {
                    if (indexAnim >= Sprites.Count)
                    {
                        indexAnim = 0;
                        sprite = Sprites[indexAnim];
                    }
                    else
                    {
                        sprite = Sprites[indexAnim];
                        indexAnim++;
                    }
                }
                else
                {
                    if (indexAnim >= Sprites.Count)
                    {
                        sprite = Sprites[Sprites.Count - 1];
                    }
                    else
                    {
                        sprite = Sprites[indexAnim];
                        indexAnim++;
                    }
                }
                AnimTimer = 0;
            }
            else
            {
                AnimTimer += Program.DTime;
            }
        }
    }
}
