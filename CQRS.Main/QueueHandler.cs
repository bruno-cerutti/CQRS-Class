using System.Collections.Concurrent;
using System.Threading;

namespace CQRS.Main
{
    public class QueueHandler : IHandle<AMessage>, IStartable, IStats
    {
		private readonly string _queueName;
		private readonly ConcurrentQueue<AMessage> _queue;
        private readonly Thread _theThread;

		IHandle<AMessage> _handler;

		public QueueHandler(IHandle<AMessage> handler, string queueName)
        {
			_handler = handler;
			_queueName = queueName;
			_queue = new ConcurrentQueue<AMessage>();
            _theThread = new Thread(ProcessOrder);
        }

		public void Handle(AMessage order)
        {
            _queue.Enqueue(order);
        }


        private void ProcessOrder()
        {
            while (true)
            {

				AMessage order;
                if (!_queue.TryDequeue(out order))
                {
                    Thread.Sleep(1);
                }
                else
                {
					_handler.Handle (order);
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