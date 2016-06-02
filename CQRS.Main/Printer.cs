using System;

namespace CQRS.Main
{
	public class Printer : IHandle<AMessage>
    {

		public void Handle(AMessage order)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(order);


        }
    }
}