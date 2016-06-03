using System;

namespace CQRS.Main
{
    public class PriceOrder : AMessage
    {
        public Order Order { get; private set; }

        public PriceOrder(Guid id, Guid correlationId, Guid causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}