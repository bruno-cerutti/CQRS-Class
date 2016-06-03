using System;

namespace CQRS.Main
{
    public class Monitor : IHandle<OrderPlaced>
    {
        public void Handle(OrderPlaced message)
        {
            Console.WriteLine($"Order number {message.CorrelationId} placed");
        }
    }
}