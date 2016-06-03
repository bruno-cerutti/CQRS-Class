using System;

namespace CQRS.Main
{
	public class Midget : IHandle<OrderPlaced>, IHandle<FoodCooked>, IHandle<OrderPriced>, IHandle<OrderPaid>
	{
		#region IHandle implementation

		public delegate void ProcessTerminatedEvent(object sender, string s);

		public event ProcessTerminatedEvent ProcessTerminated;

		public void Handle (OrderPlaced message)
		{
			_bus.PublishByType(new CookFood(Guid.NewGuid().ToString(),
				message.CorrelationId,
				message.Id,
				message.Order));
		}

		public void Handle (FoodCooked message)
		{
			_bus.PublishByType (new PriceOrder(Guid.NewGuid ().ToString (),
							 message.CorrelationId,
						     message.Id,
						     message.Order));
		}

		public void Handle (OrderPriced message)
		{

			_bus.PublishByType(new TakePayment(Guid.NewGuid().ToString(),
				message.CorrelationId,
				message.Id,
				message.Order));
		}

		public void Handle (OrderPaid message)
		{
			ProcessTerminated(this, message.CorrelationId);
		}

		#endregion

		IPublisher _bus;
		public Midget (IPublisher bus)
		{
			_bus = bus;
		}
	}
}

