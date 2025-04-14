namespace Settings
{
    public interface IMusicContent
    {
        bool IsMusicEnabled { get; }

        void EnableMusic(bool isEnabled, bool shouldSave = true);
    }
}
