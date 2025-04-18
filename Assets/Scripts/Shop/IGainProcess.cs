using System;

namespace Shop
{
    public interface IGainProcess
    {
        void Apply(IShopContent model);

        bool HasGain(IShopContent model);
    }
}