namespace CQRS.Main
{
    public class Cashier : IHandleOrder
    {
        private readonly IHandleOrder _handler;

        public Cashier(IHandleOrder handler)
        {
            _handler = handler;
        }

        public void Handle(Order order)
        {
            order.Paid = true;

            _handler.Handle(order);
        }
    }
}