using System;

namespace CQRS.Main
{
    public class Monitor : IHandle<AMessage>
    {
        public void Handle(AMessage message)
        {
            Console.WriteLine($"Order number {message.CorrelationId}. {message.GetType().Name}");
        }
    }
}