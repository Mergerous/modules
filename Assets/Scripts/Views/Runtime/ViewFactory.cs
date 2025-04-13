using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class ViewFactory
    {
        public ViewFactory(ViewsManager viewManager, IEnumerable<IViewHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                GetViewAttribute attribute = handler.GetType().GetCustomAttribute<GetViewAttribute>();

                if (attribute != null)
                {
                    handler.OnHandled(new ViewHandle(attribute.key, viewManager.CreateView, viewManager.DestroyView));
                }
            }
        }
    }
}