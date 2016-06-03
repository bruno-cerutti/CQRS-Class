using System;

namespace CQRS.Main
{
	public class Cashier : IHandle<TakePayment>
    {
	    private readonly IPublisher _publisher;

		public Cashier(IPublisher publisher)
        {
			_publisher = publisher;
            
        }

	    public void Handle(TakePayment message)
	    {
            var order = message.Order;
            order.Paid = true;

            _publisher.PublishByType(new OrderPaid(Guid.NewGuid().ToString(), message.CorrelationId, message.Id, order));
        }
    }
}