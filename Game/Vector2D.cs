using System;

namespace Game
{
    public class Vector2D
    {
        protected float x;
        protected float y;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        private float LengthSquared()
        {
            return (x * x + y * y);
        }

        public float Length()
        {
            return (float)Math.Sqrt(LengthSquared());
        }

        public Vector2D Clone()
        {
            return new Vector2D(x, y);
        }

        public void Negate()
        {
            x = -x;
            y = -y;
        }

        public Vector2D Normalize()
        {
            float length = Length();

            if (length > 0)
            {
                x = x / length;
                y = y / length;
            }
            return this;
        }

        public Vector2D add(Vector2D vec)
        {
            return new Vector2D(this.x + vec.x, this.y + vec.y);
        }

        public Vector2D multiply(float mfactor)
        {
            return new Vector2D(this.x * mfactor, this.y * mfactor);
        }

        public void incrementBy(Vector2D vec)
        {
            this.x += vec.x;
            this.y += vec.y;
        }

        public Vector2D subtract(Vector2D vec)
        {
            return new Vector2D(this.x - vec.x, this.y - vec.y);
        }

        public void decrementBy(Vector2D vec)
        {
            this.x -= vec.x;
            this.y -= vec.y;
        }

        public void scaleBy(float k)
        {
            this.x *= k;
            this.y *= k;
        }

        public float dotProduct(Vector2D vec)
        {
            return (this.x * vec.x + this.y * vec.y);
        }

        // STATIC METHODS
        public static float distance(Vector2D vec1, Vector2D vec2)
        {
            return (vec1.subtract(vec2)).Length();
        }

        public static float angleBetween(Vector2D vec1, Vector2D vec2)
        {
            return (float)Math.Acos(vec1.dotProduct(vec2) / (vec1.Length() * vec2.Length()));
        }

        public static float[] midPoint(float x1, float y1, float x2, float y2)
        {
            float[] mP = new float[2];

            if (x1 < x2)
            {
                mP[0] = x1 + (x2 - x1) / 2;
            }
            else
            {
                mP[0] = x2 + (x1 - x2) / 2;
            }

            if (y1 < y2)
            {
                mP[1] = y1 + (y2 - y1) / 2;
            }
            else
            {
                mP[1] = y2 + (y1 - y2) / 2;
            }

            return mP;
        }

        public static Vector2D zero()
        {
            return new Vector2D(0, 0);
        }

        public override string ToString()
        {
            return ("Vector2D(" + x + ", " + y + ")");
        }

    }
}
