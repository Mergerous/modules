using R3;

namespace Consumables
{
    public sealed class CurrencyModel : ICurrencyContent
    {
        private readonly ReactiveProperty<int> value = new();
        public CurrencyData Data { get; }
        public ConsumableConfig Config { get; }
        
        public Observable<int> ValueObservable => value;
        
        public int Value
        {
            get => value.Value = Data.value;
            set => Data.value = this.value.Value = value;
        }
        
        public CurrencyModel(CurrencyData data, ConsumableConfig config)
        {
            Data = data;
            Config = config;
        }
    }
}