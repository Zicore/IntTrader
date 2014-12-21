using System;
using System.Collections.Generic;

namespace IntTrader.Service
{
    public class UpdateController
    {
        private readonly Dictionary<String, UpdateEntry> _updateEntries = new Dictionary<string, UpdateEntry>();

        /// <summary>
        /// Should be called in an intervall, like every second
        /// </summary>
        public void Update(ref bool condition)
        {
            foreach (var u in _updateEntries)
            {
                u.Value.Iterate(ref condition);
            }
        }

        public UpdateEntry Register(String key, Action action, int intervall, bool immediateAction, bool requireCondition)
        {
            var updateEntry = new UpdateEntry { Key = key, Action = action, CounterMax = intervall, ImmediateAction = immediateAction, RequireCondition = requireCondition };
            _updateEntries[key] = updateEntry;
            return updateEntry;
        }

        public void Remove(String key)
        {
            _updateEntries.Remove(key);
        }
    }
}
