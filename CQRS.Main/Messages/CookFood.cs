using System;

namespace CQRS.Main
{
    public class CookFood : AMessage
    {
        public Order Order { get; private set; }

        public CookFood(Guid id, Order order) : base(id)
        {
            Order = order;
        }
    }
}