using System.Collections.Generic;
using System.Threading;

namespace CQRS.Main
{
	public class MFDispatcher : IHandle<AMessage>
    {
        private readonly IEnumerable<QueueHandler> _queues;

        public MFDispatcher(IEnumerable<QueueHandler> queues)
        {
            _queues = queues;
        }

		public void Handle(AMessage order)
        {
            while (true)
            {
                foreach (var queue in _queues)
                {
                    if (queue.QueueSize < 5)
                    {
                        queue.Handle(order);
                        return;
                    }
                }

                Thread.Sleep(1);
            }
        }
    }
}