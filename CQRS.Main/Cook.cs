using System;
using System.Collections.Generic;
using System.Threading;

namespace CQRS.Main
{
	public class Cook : IHandle<CookFood>
    {       

        private readonly Dictionary<string, string> _cookBook = new Dictionary<string, string>
        {
            {"ice cream", "milk, sugar"},
            {"pizza", "tomato, mozzarella"},
            {"pasta", "flour, ragu"}
        };

		private readonly IPublisher _publisher;

		public Cook(IPublisher publisher)
        {
			_publisher = publisher;
            
        }

        public string Name { get; set; }
	    public void Handle(CookFood message)
	    {
            var order = message.Order;
            order.CookName = Name;
            order.Ingredients = string.Empty;
            foreach (var lineItem in order.LineItems)
            {
                order.Ingredients += _cookBook[lineItem.Item];
            }

            Thread.Sleep(new Random().Next(0, 1000));

            _publisher.PublishByType(new FoodCooked(Guid.NewGuid(), message.CorrelationId, message.Id, order));
        }
    }
}