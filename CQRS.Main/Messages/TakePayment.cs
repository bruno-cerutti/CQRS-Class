using System;

namespace CQRS.Main
{
    public class TakePayment : AMessage
    {
        public Order Order { get; private set; }

        public TakePayment(Guid id, Guid correlationId, Guid causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}