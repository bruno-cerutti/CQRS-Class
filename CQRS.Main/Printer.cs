using System;

namespace CQRS.Main
{
    public class Printer : IHandleOrder
    {
        public void Handle(Order order)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(order);
        }
    }
}