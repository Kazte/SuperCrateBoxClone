using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public delegate void DrawColliderDelegate();

    public class Collider
    {

        private DrawColliderDelegate drawCollider;

        private string pixelSprite = "img/utils/pixel.png";

        public DrawColliderDelegate DrawCollider
        {
            get
            {
                return drawCollider;
            }

            set
            {
                drawCollider = value;
            }
        }

        bool isCollisionable;
        bool isCircle;

        float x;
        float y;

        float radius;

        float offsetx;
        float offsety;

        protected float sizeX { get; private set; }
        protected float sizeY { get; private set; }
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public Collider(float x, float y, float sizeX, float sizeY, float offsetx, float offsety, bool isCollisionable, bool isCircle)
        {
            this.offsetx = offsetx;
            this.offsety = offsety;

            this.sizeX = sizeX;
            this.sizeY = sizeY;

            this.X = x;
            this.Y = y;

            radius = sizeX / 2;

            this.isCollisionable = isCollisionable;
            this.isCircle = isCircle;

            drawCollider = isCircle ? new DrawColliderDelegate(DrawCircleCollider) : new DrawColliderDelegate(DrawBoxCollider);

        }

        public Collider getCollider()
        {
            return this;
        }

        private static bool CheckBoxBoxCollision(Collider collisionBox1, Collider collisionBox2)
        {
            bool collisionMaxX = (collisionBox1.X - collisionBox1.offsetx + collisionBox1.sizeX) > collisionBox2.X - collisionBox2.offsetx;
            bool collisionMinX = (collisionBox1.X - collisionBox1.offsetx) < (collisionBox2.X - collisionBox2.offsetx + collisionBox2.sizeX);

            bool collisionX = collisionMaxX && collisionMinX;

            bool collisionMaxY = (collisionBox1.Y - collisionBox1.offsety + collisionBox1.sizeY) > collisionBox2.Y - collisionBox2.offsety;
            bool collisionMinY = (collisionBox1.Y - collisionBox1.offsety) < (collisionBox2.Y - collisionBox2.offsety + collisionBox2.sizeY);

            bool collisionY = collisionMaxY && collisionMinY;

            return collisionX && collisionY;
        }

        private static bool CheckBoxCircleCollision(Collider collisionCircle, Collider collisionBox)
        {

            float deltaX = (collisionCircle.X) - Math.Max(collisionBox.X - collisionBox.offsetx, Math.Min(collisionCircle.X, collisionBox.X - collisionBox.offsetx + collisionBox.sizeX));
            float deltaY = (collisionCircle.Y) - Math.Max(collisionBox.Y - collisionBox.offsety, Math.Min(collisionCircle.Y, collisionBox.Y - collisionBox.offsety + collisionBox.sizeY));


            return Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) < Math.Pow(collisionCircle.radius, 2);
        }

        private static bool CheckCircleCircleCollision(Collider collisionCircle1, Collider collisionCircle2)
        {
            var radius = collisionCircle1.sizeX / 2 + collisionCircle2.sizeX / 2;
            var deltaX = collisionCircle1.X - collisionCircle2.X;
            var deltaY = collisionCircle1.Y - collisionCircle2.Y;
            return Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) <= Math.Pow(radius, 2);
        }

        public static bool CheckCollision(Collider collider1, Collider collider2)
        {

            if (collider1.isCollisionable && collider2.isCollisionable)
            {
                if (!collider1.isCircle && !collider2.isCircle)
                {
                    return CheckBoxBoxCollision(collider1, collider2);
                }

                Collider collisionCircle;
                Collider collisionBox;

                if (collider1.isCircle && collider2.isCircle)
                {
                    return CheckCircleCircleCollision(collider1, collider2);
                }
                if (collider1.isCircle)
                {
                    collisionCircle = collider1;
                    collisionBox = collider2;
                }
                else
                {
                    collisionCircle = collider2;
                    collisionBox = collider1;
                }
                return CheckBoxCircleCollision(collisionCircle, collisionBox);
            }
            return false;
        }

        protected void DrawBoxCollider()
        {
            for (float i = X - offsetx; i < X + offsetx; i++)
            {
                Engine.Draw(pixelSprite, i, Y - offsety);
                Engine.Draw(pixelSprite, i, Y + offsety);
            }
            for (float i = Y - offsety; i < Y + offsety; i++)
            {
                Engine.Draw(pixelSprite, X - offsetx, i);
                Engine.Draw(pixelSprite, X + offsetx, i);
            }
        }

        protected void DrawCircleCollider()
        {
            float drawX;
            float drawY;
            for (float i = 0; i <= 360; i++)
            {
                drawX = (float)Math.Cos((i) * Math.PI / 180) * radius;
                drawY = (float)Math.Sin((i) * Math.PI / 180) * radius;
                Engine.Draw(pixelSprite, X + drawX, Y + drawY);
            }
        }

    }
}


