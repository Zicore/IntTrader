using System;
using System.Threading;
using System.Threading.Tasks;

namespace IntTrader.API.Threading
{
    /// <summary>
    /// This is required to make all requests in proper order, in the order the nonce is incrementing.
    /// </summary>
    public class RequestHandler
    {
        readonly private LockFreeQueue<Action> _queue = new LockFreeQueue<Action>();
        readonly Task _task;
        private volatile bool _isEnabled = false;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        public LockFreeQueue<Action> Queue
        {
            get { return _queue; }
        }

        public RequestHandler()
        {

            _isEnabled = true;
            _task = new Task(Execute);
            _task.Start();
        }

        private void Execute()
        {
            while (_isEnabled)
            {
                Action action;
                while (_queue.Dequeue(out action))
                {
                    try
                    {
                        action.Invoke();
                    }
                    catch
                    {

                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
