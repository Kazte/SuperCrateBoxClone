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
        private List<string> sprites;
        private float speed = 0.05f;
        private bool isLoop;
        private int indexAnim;
        private float animTimer;
        private string sprite;

        public List<string> Sprites { get => sprites; set => sprites = value; }
        public float Speed { get => speed; set => speed = value; }
        public bool IsLoop { get => isLoop; set => isLoop = value; }
        public float AnimTimer { get => animTimer; set => animTimer = value; }
        public string Sprite { get => sprite; set => sprite = value; }

        public Animation(string path, bool isLoop)
        {
            sprites = new List<string>();
            this.isLoop = isLoop;
            LoadTextures(path);
        }


        void LoadTextures(string path)
        {
            DirectoryInfo myDir = new DirectoryInfo(path);
            if (myDir.GetFiles().Length > 1)
            {
                foreach (FileInfo file in myDir.GetFiles())
                {
                    sprites.Add(file.FullName);
                }
            }
            else
            {
                sprites.Add(myDir.GetFiles()[0].FullName);
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
                        Sprite = Sprites[indexAnim];
                    }
                    else
                    {
                        Sprite = Sprites[indexAnim];
                        indexAnim++;
                    }
                }
                else
                {
                    if (indexAnim >= Sprites.Count)
                    {
                        Sprite = Sprites[Sprites.Count - 1];
                    }
                    else
                    {
                        Sprite = Sprites[indexAnim];
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
