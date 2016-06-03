using System;

namespace CQRS.Main
{
	public class Cashier : IHandle<OrderPriced>
    {
		IPublisher _publisher;

		public Cashier(IPublisher publisher)
        {
			_publisher = publisher;
            
        }

		public void Handle(OrderPriced message)
        {
			var order = message.Order;
            order.Paid = true;

			_publisher.Publish (new OrderPaid(Guid.NewGuid(), order));
        }
    }
}