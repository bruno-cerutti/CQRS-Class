using System;

namespace CQRS.Main
{
    public class PriceOrder : AMessage
    {
        public Order Order { get; private set; }

        public PriceOrder(Guid id, Order order) : base(id)
        {
            Order = order;
        }
    }
}