using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System;

namespace CQRS.Main
{
	public class AssistantManager : IHandle<PriceOrder>
    {
		private readonly IPublisher _bus;

        private readonly Dictionary<string, decimal> _priceList = new Dictionary<string, decimal>
        {
            {"ice cream", 2.00m},
            {"pizza", 7.5m},
            {"pasta", 5.5m}
        };

		public AssistantManager(IPublisher bus)
        {
			_bus = bus;
        }

		public void Handle(FoodCooked message)
        {
			var order = message.Order;
            foreach (var lineItem in order.LineItems)
            {
                lineItem.Price = _priceList[lineItem.Item];
            }

            var total = order.LineItems.Sum(item => _priceList[item.Item] * item.Quantity);

            var tax = total*0.12m;

            order.Tax = tax;
            order.Total = total;

			_bus.Publish (new OrderPriced(Guid.NewGuid(), order));
        }

	    public void Handle(PriceOrder message)
	    {
	        throw new NotImplementedException();
	    }
    }
}