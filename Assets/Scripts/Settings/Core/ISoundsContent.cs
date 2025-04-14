namespace Settings
{
    public interface ISoundsContent
    {
        bool IsSoundsEnabled { get; }

        void EnableSounds(bool isEnabled, bool shouldSave = true);
    }
}
