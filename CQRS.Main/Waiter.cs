using System;
using System.Collections.Generic;

namespace CQRS.Main
{
    public class Waiter
    {
        private readonly IHandleOrder _handler;

        public Waiter(IHandleOrder handler)
        {
            _handler = handler;
        }

        public int PlaceOrder(int tableNumber, List<Tuple<int, string>> lineItems)
        {
            var order = new Order(new Random().Next());
            foreach (var lineItem in lineItems)
            {
                order.AddItem(lineItem.Item1, lineItem.Item2);
            }
            _handler.Handle(order);

            return order.Id;
        }
    }
}