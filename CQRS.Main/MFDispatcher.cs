using System.Collections.Generic;
using System.Threading;

namespace CQRS.Main
{
	public class MFDispatcher<TMessage> : IHandle<TMessage>
    {
        private readonly IEnumerable<QueueHandler<TMessage>> _queues;

        public MFDispatcher(IEnumerable<QueueHandler<TMessage>> queues)
        {
            _queues = queues;
        }

		public void Handle(TMessage message)
        {
            while (true)
            {
                foreach (var queue in _queues)
                {
                    if (queue.QueueSize < 5)
                    {
                        queue.Handle(message);
                        return;
                    }
                }

                Thread.Sleep(1);
            }
        }
    }
}