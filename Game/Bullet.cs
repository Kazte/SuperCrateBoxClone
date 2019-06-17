namespace Game
{
    public class Bullet : GameObject, IUpdateable, IRenderizable
    {
        private float velocity = 300f;
        private float lifeTime = 3f;
        private float timer;
        int face;

        public event SimpleEventHandler<Bullet> OnDeactivate;


        Collider collider;

        public bool Destroyed { get; set; }
        public float Radius { get; set; }

        public Bullet(Vector2D position, float angle) : base(position, angle)    
        {
            collider = new Collider(position.X, position.Y, 8, 8, 4, 4, true, true);
        }

        public void Init(float x, float y, int face, int speed)
        {
            position.X = x;
            position.Y = y;
            this.face = face;
            this.velocity = speed;

            timer = 0;
            Destroyed = false;
            Program.Bullets.Add(this);
        }

        public void Render()
        {
            if (!Destroyed)
            {
                Engine.Draw(@"img\weapons\bullet.png", position.X, position.Y, 1, 1, angle, 4, 4);
            }
        }

        public void Update()
        {
            collider.X = position.X;
            collider.Y = position.Y;

            if (timer >= lifeTime)
            {
                Desactivate();
            }
            else if (!Destroyed)
            {
                
                timer += Program.DTime;
                Movement();
                CheckCollisions();
            }
        }

        // Funcion de destruir la bala, la saca de la lista de balas del Program
        private void Desactivate()
        {
            Destroyed = true;
            Program.Bullets.Remove(this);

            if (OnDeactivate != null)
            {
                OnDeactivate.Invoke(this);
            }
        }

        // Me muevo constantemente hacia arriba
        private void Movement()
        {
            position.X += velocity * Program.DTime * face;
        }

        // Funcion de chequear colisiones con enemigos
        private void CheckCollisions()
        {
            for (int i = 0; i < Program.Enemies.Count; i++)
            {
                CheckCollision(Program.Enemies[i]);
            }
        }

        private void CheckCollision(Enemy enemy)
        {
            if (Collider.CheckCollision(collider, enemy.Collider)){
                
                Engine.Debug("Enemigo Destruido");
                Desactivate();
            }
        }
    }
}
