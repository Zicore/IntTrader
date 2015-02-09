using System;
using System.Collections.ObjectModel;
using System.Linq;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Currency;

namespace IntTrader.ViewModel
{
    public class PairManageViewModel : ExchangeViewModelBase
    {
        private readonly ObservableCollection<PairViewModel> _pairs = new ObservableCollection<PairViewModel>();
        public ObservableCollection<PairViewModel> Pairs
        {
            get { return _pairs; }
        }

        public event EventHandler PairChanged;

        protected virtual void OnPairChanged()
        {
            EventHandler handler = PairChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public PairManageViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
            foreach (var supportedPair in Exchange.PairManager.SupportedPairs.Values)
            {
                Pairs.Add(PairViewModel.FromPair(supportedPair));
            }

            SelectedPair = Pairs.FirstOrDefault();
        }

        private PairViewModel _selectedPair;
        public PairViewModel SelectedPair
        {
            get { return _selectedPair; }
            set
            {
                _selectedPair = value;
                OnPairChanged();
            }
        }

        public void Select(PairBase pair)
        {
            foreach (var pairViewModel in Pairs)
            {
                if (pairViewModel.Pair.Key == pair.Key)
                {
                    this.SelectedPair = pairViewModel;
                    break;
                }
            }
        }
    }
}
