using System;
using CQRS.Main.Messages;

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
		    _bus.PublishByType(new SendToMeIn5(Guid.NewGuid().ToString(),
		        message.CorrelationId,
		        message.Id,
		        new RetryCooking(Guid.NewGuid().ToString(), message.CorrelationId, message.Id, message.Order)));
		}

		public void Handle (FoodCooked message)
		{
		    _alredyCooked = true;
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
            _bus.PublishByType(new PrintOrder(Guid.NewGuid().ToString(),
                message.CorrelationId,
                message.Id,
                message.Order));
			Console.WriteLine ("Process terminated!");
            ProcessTerminated?.Invoke(this, message.CorrelationId);
        }

        public void Handle(RetryCooking message)
        {
            if (!_alredyCooked)
            {
                Console.WriteLine("Retry cooking");
                _bus.PublishByType(new CookFood(Guid.NewGuid().ToString(),
                    message.CorrelationId,
                    message.Id,
                    message.Order));
            }
        }

        public void Handle(Message message){
		}

		#endregion

		IPublisher _bus;
	    private bool _alredyCooked;

	    public Midget (IPublisher bus)
		{
			_bus = bus;
		}

	}
}

