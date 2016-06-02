using System;
using System.Collections.Generic;
using System.Threading;

namespace CQRS.Main
{
    public class Cook : IHandleOrder
    {
        private readonly IHandleOrder _handler;

        private readonly Dictionary<string, string> _cookBook = new Dictionary<string, string>
        {
            {"ice cream", "milk, sugar"},
            {"pizza", "tomato, mozzarella"},
            {"pasta", "flour, ragu"}
        };

        public Cook(IHandleOrder handler)
        {
            _handler = handler;
        }

        public void Handle(Order order)
        {
            order.CookName = Name;
            order.Ingredients = string.Empty;
            foreach (var lineItem in order.LineItems)
            {
                order.Ingredients += _cookBook[lineItem.Item];
            }

            Thread.Sleep(new Random().Next(0,1000));

            _handler.Handle(order);
        }

        public string Name { get; set; }
    }
}