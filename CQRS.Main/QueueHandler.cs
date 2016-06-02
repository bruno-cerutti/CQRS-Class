using System.Collections.Concurrent;
using System.Threading;

namespace CQRS.Main
{
    public class QueueHandler : IHandleOrder, IStartable
    {
        private readonly IHandleOrder _handler;
        private readonly ConcurrentQueue<Order> _queue;
        private readonly Thread _theThread;

        public QueueHandler(IHandleOrder handler)
        {
            _handler = handler;
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

        public void Start()
        {
            _theThread.Start();
        }
    }
}