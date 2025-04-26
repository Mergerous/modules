namespace Modules.Settings
{
    public interface IToggleSettingsProcessor
    {
        void Process(bool isOn, ISettingsItemModel model);
    }
}