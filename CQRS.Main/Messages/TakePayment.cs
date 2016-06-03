using System;

namespace CQRS.Main
{
    public class TakePayment : AMessage
    {
        public Order Order { get; private set; }

        public TakePayment(Guid id, Order order) : base(id)
        {
            Order = order;
        }
    }
}