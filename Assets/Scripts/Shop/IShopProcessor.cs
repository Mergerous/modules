namespace Shop
{
    public interface IShopProcessor
    {
        void Buy(IShopContent shopContent, params ISenderData[] senderData);
        bool HasGain(IShopContent shopContent);
        void Apply(IShopContent shopContent);
    }
}