using System;

namespace IntTrader.API.Base.Exchange.Base
{
    public class TypeBase
    {
        public TypeBase(String value, String name)
        {
            this.Value = value;
            this.Name = name;
        }

        private String _value;
        private String _name;
        private String _description;

        public string Value
        {
            get { return _value; }
            protected set { _value = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}
