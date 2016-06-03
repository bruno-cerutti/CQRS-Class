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

		private IPublisher _publisher;

		public Cook(IPublisher publisher)
        {
			_publisher = publisher;
            
        }

		public void Handle(OrderPlaced orderPlaced)
        {
			var order = orderPlaced.Order;
            order.CookName = Name;
            order.Ingredients = string.Empty;
            foreach (var lineItem in order.LineItems)
            {
                order.Ingredients += _cookBook[lineItem.Item];
            }

            Thread.Sleep(new Random().Next(0,1000));

			_publisher.Publish (new FoodCooked(Guid.NewGuid(), order));
        }

        public string Name { get; set; }
	    public void Handle(CookFood message)
	    {
	        throw new NotImplementedException();
	    }
    }
}