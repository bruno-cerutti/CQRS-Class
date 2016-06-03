﻿using System;

namespace CQRS.Main
{
	public class Midget : IMidget
	{
		#region IHandle implementation


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
			Console.WriteLine("Order priced!");
			_bus.PublishByType(new TakePayment(Guid.NewGuid().ToString(),
				message.CorrelationId,
				message.Id,
				message.Order));
		}

		public void Handle (OrderPaid message)
		{
			Console.WriteLine ("Process terminated!");
			ProcessTerminated(this, message.CorrelationId);
		}

		public void Handle(Message message){
		}

		#endregion

		IPublisher _bus;
		public Midget (IPublisher bus)
		{
			_bus = bus;
		}
	}
}

