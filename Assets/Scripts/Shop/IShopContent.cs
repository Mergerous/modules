namespace Shop
{
    public interface IShopContent
    {
        IRequirement Requirement { get; }
        
        IDescription Description { get; }
        
        bool TryGetGain<T>(out T gain) where T : IGain;

        void Process(ShopResult result);
    }
}