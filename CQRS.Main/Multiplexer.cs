using System.Collections.Generic;

namespace CQRS.Main
{
    public class Multiplexer : IHandleOrder
    {
        private readonly IEnumerable<IHandleOrder> _handlers;

        public Multiplexer(IEnumerable<IHandleOrder> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(Order order)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(order);
            }
        }
    }
}