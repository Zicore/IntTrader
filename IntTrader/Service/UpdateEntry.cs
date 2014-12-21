using System;
using NLog;

namespace IntTrader.Service
{
    public class UpdateEntry
    {
        public UpdateEntry()
        {
            RequireCondition = true;
        }

        private String _key;
        private int _counter = 0;
        private int _counterMax = 0;

        public int Counter
        {
            get { return _counter; }
            private set { _counter = value; }
        }

        public int CounterMax
        {
            get { return _counterMax; }
            set { _counterMax = value; }
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public UpdateEntry SetRequireCondition()
        {
            RequireCondition = true;
            return this;
        }

        public bool RequireCondition { get; set; }
        public bool ImmediateAction { get; set; }
        public Action Action { get; set; }

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public void Iterate(ref bool condition)
        {
            if (Counter++ >= CounterMax || ImmediateAction)
            {

                if (condition || !RequireCondition)
                {
                    Log.Info("Action:{0} Condition:{1}", Key, condition);
                    Action();
                    OnActionRaised();
                }
                Counter = 0;
                ImmediateAction = false;
            }
        }

        public event EventHandler ActionRaised;

        protected virtual void OnActionRaised()
        {
            EventHandler handler = ActionRaised;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
