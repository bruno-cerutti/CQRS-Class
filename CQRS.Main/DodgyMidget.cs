using System;
using CQRS.Main.Messages;

namespace CQRS.Main
{
	public class DodgyMidget : IMidget
	{
        public event ProcessTerminatedEvent ProcessTerminated;
        
	    private readonly IPublisher _bus;
	    private bool _alreadyCooked;
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
		    _alreadyCooked = true;
            _bus.PublishByType(new PrintOrder(Guid.NewGuid().ToString(),
                message.CorrelationId,
                message.Id,
                message.Order));
            Console.WriteLine("Process terminated!");
		    ProcessTerminated?.Invoke (this, message.CorrelationId);
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
			_bus.PublishByType(new CookFood(Guid.NewGuid().ToString(),
				message.CorrelationId,
				message.Id,
				message.Order));
            _bus.PublishByType(new SendToMeIn5(Guid.NewGuid().ToString(),
                message.CorrelationId,
                message.Id,
                new RetryCooking(Guid.NewGuid().ToString(), message.CorrelationId, message.Id, message.Order)));
        }

		public void Handle(Message message){
		}


	    public void Handle(RetryCooking message)
	    {
            Console.WriteLine("Retry cooking");
            _bus.PublishByType(new CookFood(Guid.NewGuid().ToString(),
                message.CorrelationId,
                message.Id,
                message.Order));
        }
	}
}

