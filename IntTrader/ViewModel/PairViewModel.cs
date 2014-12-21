using System;
using IntTrader.API.Currency;

namespace IntTrader.ViewModel
{
    public class PairViewModel : ViewModelBase
    {
        public PairViewModel()
        {

        }

        public static PairViewModel FromPair(PairBase pair)
        {
            return new PairViewModel
                {
                    Name = pair.Name,
                    Description = pair.Description,
                    Pair = pair
                };
        }

        private string _name;
        private string _description;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Pair.NamePair, Pair.SymbolPair);
        }
    }
}
