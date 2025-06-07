using System;
using System.Collections.Generic;
using Modules.Architecture.Components;

namespace Modules.Architecture.Interfaces
{
    public interface IEntity
    {
        Dictionary<Type, IComponent> Components { get; }

        bool TryGetBaseComponent<T>(out T result);
    }
}