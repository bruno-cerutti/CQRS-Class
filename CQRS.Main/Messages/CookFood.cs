using System;

namespace CQRS.Main
{
    public class CookFood : AMessage
    {
        public Order Order { get; private set; }

        public CookFood(Guid id, Guid correlationId, Guid causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}