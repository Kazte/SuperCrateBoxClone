using System.Collections.Generic;
using System.Linq;

namespace Game
{
    class Text
    {
        public char[] charText { get; private set; }
        public List<Texture> charTextures { get; private set; }
        public float x { get; private set; }
        public float y { get; private set; }
        public float height { get; private set; }
        public float width { get; private set; }

        private float defaultFontWidth = 40;
        private float defaultfontHeight = 54;
        private float scaleX;
        private float scaleY;

        public Text(string text, float x, float y, float width = 40, float height = 54)
        {
            charText = text.ToArray();
            this.x = x;

            this.height = height;
            this.width = width;

            scaleX = width / defaultFontWidth;
            scaleY = height / defaultfontHeight;

            this.y = y;
            loadTextures();
        }

        void loadTextures()
        {
            charTextures = new List<Texture>();
            foreach (char c in charText)
            {
                if (c.Equals(' '))
                {
                    charTextures.Add(Engine.GetTexture("img/font/space.png"));
                }
                else if (c.Equals('.'))
                {
                    charTextures.Add(Engine.GetTexture("img/font/dot.png"));
                }
                else if (c.Equals('/'))
                {
                    charTextures.Add(Engine.GetTexture("img/font/slash.png"));
                }
                else
                {
                    charTextures.Add(Engine.GetTexture("img/font/" + c + ".png"));
                }
            }
        }

        public void changeText(string newText)
        {
            charText = newText.ToArray();
            loadTextures();
        }

        public void drawText()
        {
            float xOffSet = 0;
            foreach (Texture texture in charTextures)
            {
                Engine.Draw(texture, x + xOffSet, y, scaleX, scaleY, 0, defaultFontWidth / 2, defaultfontHeight / 2);
                xOffSet += defaultFontWidth - defaultFontWidth * (1 - scaleX);
            }
        }

    }
}