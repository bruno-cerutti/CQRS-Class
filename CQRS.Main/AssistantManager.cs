using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace CQRS.Main
{
    public class AssistantManager : IHandleOrder
    {
        private readonly IHandleOrder _handler;

        private readonly Dictionary<string, decimal> _priceList = new Dictionary<string, decimal>
        {
            {"ice cream", 2.00m},
            {"pizza", 7.5m},
            {"pasta", 5.5m}
        };

        public AssistantManager(IHandleOrder handler)
        {
            _handler = handler;
        }

        public void Handle(Order order)
        {
            foreach (var lineItem in order.LineItems)
            {
                lineItem.Price = _priceList[lineItem.Item];
            }

            var total = order.LineItems.Sum(item => _priceList[item.Item] * item.Quantity);

            var tax = total*0.12m;

            order.Tax = tax;
            order.Total = total;

            _handler.Handle(order);
        }
    }
}