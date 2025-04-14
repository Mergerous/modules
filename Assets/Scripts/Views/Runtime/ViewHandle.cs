using System;

namespace Modules.Views
{
    public sealed class ViewHandle : IDisposable
    {
        private event Func<string, View> onCreate;
        private event Action<View> onDestroy;
        private readonly string key;
        
        private View view;
        
        public View View => view ??= onCreate?.Invoke(key);

        public ViewHandle(string key, Func<string, View> onCreate, Action<View> onDestroy)
        {
            this.key = key;
            this.onCreate = onCreate;
            this.onDestroy = onDestroy;
        }

        public void Dispose()
        {
            onDestroy?.Invoke(view);
            view = default;
        }
    }
}