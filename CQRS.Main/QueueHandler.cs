using System.Collections.Concurrent;
using System.Threading;

namespace CQRS.Main
{
    public class QueueHandler : IHandleOrder, IStartable, IStats
    {
        private readonly IHandleOrder _handler;
        private readonly string _queueName;
        private readonly ConcurrentQueue<Order> _queue;
        private readonly Thread _theThread;

        public QueueHandler(IHandleOrder handler, string queueName)
        {
            _handler = handler;
            _queueName = queueName;
            _queue = new ConcurrentQueue<Order>();
            _theThread = new Thread(ProcessOrder);
        }

        public void Handle(Order order)
        {
            _queue.Enqueue(order);
        }


        private void ProcessOrder()
        {
            while (true)
            {

                Order order;
                if (!_queue.TryDequeue(out order))
                {
                    Thread.Sleep(1);
                }
                else
                {
                    _handler.Handle(order);
                }

            }
        }

        public int QueueSize => _queue.Count;

        public void Start()
        {
            _theThread.Start();
        }


        public string GetStats()
        {
            return $"{_queueName} queue size: {_queue.Count}";
        }
    }
}