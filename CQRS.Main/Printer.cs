using System;

namespace CQRS.Main
{
	public class Printer : IHandle<OrderPaid>
    {

		public void Handle(OrderPaid message)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(message.Order);


        }
    }
}