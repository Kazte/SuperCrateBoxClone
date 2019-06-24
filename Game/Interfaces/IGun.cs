namespace Game.Interfaces
{
    public interface IGun
    {
        int MaxAmmo { get; set; }
        int CurrentAmmo { get; set; }
        bool Automatic { get; set; }
        void Shoot();
        void Reload();
    }
}
