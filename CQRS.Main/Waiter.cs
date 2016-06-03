using System;
using System.Collections.Generic;

namespace CQRS.Main
{
    public class Waiter
    {
		private readonly IPublisher _bus;

		public Waiter(IPublisher bus)
        {
			_bus = bus;
        }

        public int PlaceOrder(int tableNumber, List<Tuple<int, string>> lineItems)
        {
            var order = new Order(new Random().Next());
            foreach (var lineItem in lineItems)
            {
                order.AddItem(lineItem.Item1, lineItem.Item2);
            }
            _bus.PublishByType(new OrderPlaced(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, order));

            return order.Id;
        }
    }
}