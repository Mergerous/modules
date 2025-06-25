namespace Modules.Views
{
    public interface IViewFactory
    {
        // TODO Create IView interface
        View CreateView(string key);

        void DestroyView(View view);
    }
}