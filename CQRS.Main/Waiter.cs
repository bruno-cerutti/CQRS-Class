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

        public string PlaceOrder(int tableNumber, List<Tuple<int, string>> lineItems)
        {
            var order = new Order(new Random().Next());
            foreach (var lineItem in lineItems)
            {
                order.AddItem(lineItem.Item1, lineItem.Item2);
            }
            _bus.PublishByType(new OrderPlaced(Guid.NewGuid().ToString(), order.Id.ToString(), string.Empty, order));

            return order.Id.ToString();
        }
    }
}