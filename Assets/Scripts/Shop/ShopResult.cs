namespace Shop
{
    public struct ShopResult
    {
        public readonly bool isCompleted;
        public readonly IResultData resultData;

        public ShopResult(bool isCompleted, IResultData resultData = null)
        {
            this.isCompleted = isCompleted;
            this.resultData = resultData;
        }
    }
}