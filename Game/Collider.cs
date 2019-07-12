using System;

namespace Game
{
    public delegate void DrawColliderDelegate();

    public class Collider
    {
        private DrawColliderDelegate drawCollider;

        private string pixelSprite = "img/utils/pixel.png";

        public DrawColliderDelegate DrawCollider
        {
            get { return drawCollider; }

            set { drawCollider = value; }
        }

        bool isCollisionable;
        bool isCircle;

        float x;
        float y;

        float radius;

        float offsetX;
        float offsetY;

        float sizeX;
        float sizeY;

        public float X
        {
            get => x;
            set => x = value;
        }

        public float Y
        {
            get => y;
            set => y = value;
        }

        public float SizeX
        {
            get => sizeX;
            set => sizeX = value;
        }

        public float SizeY
        {
            get => sizeY;
            set => sizeY = value;
        }

        public float OffsetX
        {
            get => offsetX;
            set => offsetX = value;
        }

        public float OffsetY
        {
            get => offsetY;
            set => offsetY = value;
        }

        public Collider(float x, float y, float sizeX, float sizeY, float offsetX, float offsetY, bool isCollisionable,
            bool isCircle)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;

            this.OffsetX = sizeX / 2;
            this.OffsetY = sizeY / 2;


            this.X = x;
            this.Y = y;

            radius = sizeX / 2;

            this.isCollisionable = isCollisionable;
            this.isCircle = isCircle;

            drawCollider = isCircle
                ? new DrawColliderDelegate(DrawCircleCollider)
                : new DrawColliderDelegate(DrawBoxCollider);
        }

        public Collider GetCollider()
        {
            return this;
        }

        private static bool CheckBoxBoxCollision(Collider collisionBox1, Collider collisionBox2)
        {
            bool collisionMaxX = (collisionBox1.X - collisionBox1.OffsetX + collisionBox1.SizeX) >
                                 collisionBox2.X - collisionBox2.OffsetY;
            bool collisionMinX = (collisionBox1.X - collisionBox1.OffsetX) <
                                 (collisionBox2.X - collisionBox2.OffsetX + collisionBox2.SizeX);

            bool collisionX = collisionMaxX && collisionMinX;

            bool collisionMaxY = (collisionBox1.Y - collisionBox1.OffsetY + collisionBox1.SizeY) >
                                 collisionBox2.Y - collisionBox2.OffsetY;
            bool collisionMinY = (collisionBox1.Y - collisionBox1.OffsetY) <
                                 (collisionBox2.Y - collisionBox2.OffsetY + collisionBox2.SizeY);

            bool collisionY = collisionMaxY && collisionMinY;

            return collisionX && collisionY;
        }

        private static bool CheckBoxCircleCollision(Collider collisionCircle, Collider collisionBox)
        {
            float deltaX = (collisionCircle.X) - Math.Max(collisionBox.X - collisionBox.OffsetX,
                               Math.Min(collisionCircle.X, collisionBox.X - collisionBox.OffsetX + collisionBox.SizeX));
            float deltaY = (collisionCircle.Y) - Math.Max(collisionBox.Y - collisionBox.OffsetY,
                               Math.Min(collisionCircle.Y, collisionBox.Y - collisionBox.OffsetY + collisionBox.SizeY));


            return Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) < Math.Pow(collisionCircle.radius, 2);
        }

        private static bool CheckCircleCircleCollision(Collider collisionCircle1, Collider collisionCircle2)
        {
            var radius = collisionCircle1.SizeX / 2 + collisionCircle2.SizeX / 2;
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
            for (float i = X - OffsetX; i < X + OffsetX; i++)
            {
                Engine.Draw(pixelSprite, i, Y - OffsetY);
                Engine.Draw(pixelSprite, i, Y + OffsetY);
            }

            for (float i = Y - OffsetY; i < Y + OffsetY; i++)
            {
                Engine.Draw(pixelSprite, X - OffsetX, i);
                Engine.Draw(pixelSprite, X + OffsetX, i);
            }
        }

        protected void DrawCircleCollider()
        {
            float drawX;
            float drawY;
            for (float i = 0; i <= 360; i++)
            {
                drawX = (float) Math.Cos((i) * Math.PI / 180) * radius;
                drawY = (float) Math.Sin((i) * Math.PI / 180) * radius;
                Engine.Draw(pixelSprite, X + drawX, Y + drawY);
            }
        }
    }
}