using System;

namespace CQRS.Main
{
    public class TakePayment : AMessage
    {
        public Order Order { get; private set; }

        public TakePayment(string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}