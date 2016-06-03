using System;

namespace CQRS.Main
{
	public class MidgetHouse : IHandle<OrderPlaced>
	{
		MidgetFactory _factory;

		IPublisher _bus;

		public MidgetHouse (IPublisher bus, MidgetFactory factory)
		{
			_factory = factory;
			_bus = bus;
			
		}

		#region IHandle implementation
		public void Handle (OrderPlaced message)
		{
			//var midget = new Midget (_bus);
			var midget = _factory.CreateMidget(message.Order);

			midget.ProcessTerminated += (sender, s) => _bus.UnsubscribeByCorrelationId(s);
			_bus.SubscribeByCorrelationId<OrderPlaced>(message.CorrelationId, midget);
			midget.ProcessTerminated += (sender, s) => _bus.UnsubscribeByCorrelationId(s);
			_bus.SubscribeByCorrelationId<OrderPlaced>(message.CorrelationId, midget);

		}
		#endregion
		
	}
}

