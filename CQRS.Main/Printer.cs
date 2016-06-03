using System;
using CQRS.Main.Messages;

namespace CQRS.Main
{
	public class Printer : IHandle<PrintOrder>
	{
	    private static readonly object _lock = new object();
	    private int _counter;

		public void Handle(PrintOrder message)
        {
		    lock (_lock)
		    {
		        ++_counter;
		    }

            Console.WriteLine(DateTime.Now);
            //Console.WriteLine(message.Order);
            Console.WriteLine($"Orders payed: {_counter}");


        }
    }
}