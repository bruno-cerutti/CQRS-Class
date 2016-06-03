using System;

namespace CQRS.Main
{
    public class CookFood : AMessage
    {
        public Order Order { get; private set; }

        public CookFood(string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}