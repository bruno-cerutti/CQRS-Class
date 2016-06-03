using System;

namespace CQRS.Main
{
    public class PriceOrder : AMessage
    {
        public Order Order { get; private set; }

        public PriceOrder(string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}