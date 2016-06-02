using System.Collections.Generic;

namespace CQRS.Main
{
	public class Multiplexer : IHandle<AMessage>
    {
		private readonly IEnumerable<IHandle<AMessage>> _handlers;

		public Multiplexer(IEnumerable<IHandle<AMessage>> handlers)
        {
            _handlers = handlers;
        }

		public void Handle(AMessage order)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(order);
            }
        }
    }
}