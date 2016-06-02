using System.Collections.Generic;

namespace CQRS.Main
{
    public class RRDispatcher : IHandleOrder
    {
        private readonly Queue<IHandleOrder> _queue;

        public RRDispatcher(IEnumerable<IHandleOrder> handlers)
        {
            _queue = new Queue<IHandleOrder>(handlers);
        }

        public void Handle(Order order)
        {
            var handler = _queue.Dequeue();
            handler.Handle(order);
            _queue.Enqueue(handler);
        }
    }
}