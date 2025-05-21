using R3;

namespace Consumables.Currencies
{
    public sealed class CurrencyModel : ICurrencyContent
    {
        public readonly CurrencyData data;
        public readonly CurrencyConfig config;

        private readonly ReactiveProperty<int> value;

        public Observable<int> ValueObservable => value;

        public int Value
        {
            get => value.Value;
            set => this.value.Value = value;
        }

        public CurrencyModel(CurrencyData data, CurrencyConfig config)
        {
            this.data = data;
            this.config = config;

            value = new ReactiveProperty<int>(data.value);

            value.Subscribe(value => data.value = value);
        }
    }
}