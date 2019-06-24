namespace Game
{
    public interface IRenderizable
    {
        string Sprite { get; set; }

        void Render();
    }
}
