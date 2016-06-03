using System;

namespace CQRS.Main
{
	public class MidgetHouse : IHandle<OrderPlaced>
	{
		IPublisher _bus;

		public MidgetHouse (IPublisher bus)
		{
			_bus = bus;
			
		}

		#region IHandle implementation
		public void Handle (OrderPlaced message)
		{
			var midget = new Midget (_bus);
			midget.ProcessTerminated += (object sender, string s) => _bus.UnsubscribeByCorrelationId(s);
			_bus.SubscribeByCorrelationId<OrderPlaced>(message.CorrelationId, midget);
		}
		#endregion
		
	}
}

