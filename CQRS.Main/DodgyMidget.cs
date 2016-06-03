using System;

namespace CQRS.Main
{
	public class DodgyMidget : IMidget
	{

		public event ProcessTerminatedEvent ProcessTerminated;


		IPublisher _bus;
		public DodgyMidget (IPublisher bus)
		{
			_bus = bus;
		}

		public void Handle (OrderPlaced message)
		{
			_bus.PublishByType(new PriceOrder(Guid.NewGuid().ToString(),
				message.CorrelationId,
				message.Id,
				message.Order));
		}

		public void Handle (FoodCooked message)
		{
			ProcessTerminated (this, message.CorrelationId);
		}

		public void Handle (OrderPriced message)
		{
			Console.WriteLine("Order priced!");
			_bus.PublishByType(new TakePayment(Guid.NewGuid().ToString(),
				message.CorrelationId,
				message.Id,
				message.Order));
		}

		public void Handle (OrderPaid message)
		{
			_bus.PublishByType(new CookFood(Guid.NewGuid().ToString(),
				message.CorrelationId,
				message.Id,
				message.Order));
		}

		public void Handle(Message message){
		}



	}
}

