using System;
using System.Linq;
using System.Timers;
using CQRS.Main;

namespace Restaurant.Console
{
    class Program
    {
        private static Timer _theTimer;
        private static QueueHandler _cashierQueue;
        private static QueueHandler _assistantQueue;
        private static QueueHandler _concurrentQueue;
        private static QueueHandler _concurrentQueue2;
        private static QueueHandler _concurrentQueue3;
        private static QueueHandler kitchenQueue;

        static void Main(string[] args)
        {
            _theTimer = new Timer
            {
                AutoReset = true,
                Interval = 500
            };
            
            _theTimer.Elapsed += _theTimer_Elapsed;

			var bus = new TopicBasedPubSub ();

			var printer = new Printer();
			bus.Subscribe (printer);

			var cashier = new Cashier(bus);
			bus.Subscribe (cashier);
			_cashierQueue = new QueueHandler(cashier, "Cashier");


			var assistant = new AssistantManager(bus);		
			_assistantQueue = new QueueHandler(assistant);
			bus.Subscribe (assistant);

			var cook = new Cook(bus) {Name = "John"};
			var cook2 = new Cook(bus) { Name = "Jio" };
			var cook3 = new Cook(bus) { Name = "Jack" };

            //var multiplexer = new Multiplexer(new []{cook, cook, cook});
			_concurrentQueue = new QueueHandler(cook, cook.Name);
			_concurrentQueue2 = new QueueHandler(cook2, cook2.Name);
			_concurrentQueue3 = new QueueHandler(cook3, cook3.Name);



            //var dispatcher = new RRDispatcher(new[] { concurrentQueue, concurrentQueue2, concurrentQueue3 });
            var mfDispatcher = new MFDispatcher(new[] { _concurrentQueue, _concurrentQueue2, _concurrentQueue3 });



			kitchenQueue = new QueueHandler(mfDispatcher, "Waiter");
			bus.Subscribe (OrderPlaced, kitchenQueue);

			var waiter = new Waiter(bus);
            _concurrentQueue.Start();
            _concurrentQueue2.Start();
            _concurrentQueue3.Start();
            _assistantQueue.Start();
            kitchenQueue.Start();
            _cashierQueue.Start();

            _theTimer.Start();

            var items = new[]
            {
                new Tuple<int, string>(2, "pizza"),
                new Tuple<int, string>(1, "ice cream"),
            };

            //Parallel.For(1, 10, (i) => waiter.PlaceOrder(10, items.ToList()));
            for (int i = 0; i < 100; i++)
            {
                waiter.PlaceOrder(10, items.ToList());
            }

        }

        private static void _theTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
			System.Console.WriteLine ("========================================");
            System.Console.WriteLine(kitchenQueue.GetStats());
            System.Console.WriteLine(_concurrentQueue.GetStats());
            System.Console.WriteLine(_concurrentQueue2.GetStats());
            System.Console.WriteLine(_concurrentQueue3.GetStats());
            System.Console.WriteLine(_assistantQueue.GetStats());
            System.Console.WriteLine(_cashierQueue.GetStats());
			System.Console.WriteLine ("========================================");
        }
    }
}
