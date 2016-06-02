using System.Collections.Generic;

namespace CQRS.Main
{
	public class RRDispatcher : IHandle<AMessage>, IStats
    {
		private readonly Queue<IHandle<AMessage>> _queue;

		public RRDispatcher(IEnumerable<IHandle<AMessage>> handlers)
        {
			_queue = new Queue<IHandle<AMessage>>(handlers);
        }

		public void Handle(AMessage order)
        {
            var handler = _queue.Dequeue();
            handler.Handle(order);
            _queue.Enqueue(handler);
        }

        public string GetStats()
        {
            return $"Dispatcher queue size: {_queue.Count}";
        }
    }
}