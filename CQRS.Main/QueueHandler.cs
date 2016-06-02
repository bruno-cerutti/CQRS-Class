using System.Collections.Concurrent;
using System.Threading;

namespace CQRS.Main
{
    public class QueueHandler<TMessage> : IHandle<TMessage>, IStartable, IStats
    {
		private readonly string _queueName;
		private readonly ConcurrentQueue<TMessage> _queue;
        private readonly Thread _theThread;

        private readonly IHandle<TMessage> _handler;

		public QueueHandler(IHandle<TMessage> handler, string queueName)
        {
			_handler = handler;
			_queueName = queueName;
			_queue = new ConcurrentQueue<TMessage>();
            _theThread = new Thread(ProcessOrder);
        }

        public void Handle(TMessage message)
        {
            _queue.Enqueue(message);
        }

        private void ProcessOrder()
        {
            while (true)
            {

                TMessage message;
                if (!_queue.TryDequeue(out message))
                {
                    Thread.Sleep(1);
                }
                else
                {
					_handler.Handle (message);
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