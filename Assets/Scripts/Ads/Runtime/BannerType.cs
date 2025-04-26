namespace Modules.Ads
{
    public enum BannerType
    {
        None = 0,
        Bottom = 1 << 1,
        Top = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4
    }
}