using System;
using System.Linq;
using System.Timers;
using CQRS.Main;

namespace Restaurant.Console
{
    class Program
    {
        private static Timer _theTimer;
        private static QueueHandler<TakePayment> _cashierQueue;
        private static QueueHandler<PriceOrder> _assistantQueue;
        private static QueueHandler<CookFood> _concurrentQueue;
        private static QueueHandler<CookFood> _concurrentQueue2;
        private static QueueHandler<CookFood> _concurrentQueue3;
        private static QueueHandler<CookFood> _kitchenQueue;

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
			bus.SubscribeByType (printer);

			var cashier = new Cashier(bus);
			bus.SubscribeByType (cashier);
			_cashierQueue = new QueueHandler<TakePayment>(cashier, "Cashier");


			var assistant = new AssistantManager(bus);		
			_assistantQueue = new QueueHandler<PriceOrder>(assistant, "Assistant");
			bus.SubscribeByType (assistant);

			var cook = new Cook(bus) {Name = "John"};
			var cook2 = new Cook(bus) { Name = "Jio" };
			var cook3 = new Cook(bus) { Name = "Jack" };

            //var multiplexer = new Multiplexer(new []{cook, cook, cook});
			_concurrentQueue = new QueueHandler<CookFood>(cook, cook.Name);
			_concurrentQueue2 = new QueueHandler<CookFood>(cook2, cook2.Name);
			_concurrentQueue3 = new QueueHandler<CookFood>(cook3, cook3.Name);
            
            //var dispatcher = new RRDispatcher(new[] { concurrentQueue, concurrentQueue2, concurrentQueue3 });
            var mfDispatcher = new MFDispatcher<CookFood>(new[] { _concurrentQueue, _concurrentQueue2, _concurrentQueue3 });
            
			_kitchenQueue = new QueueHandler<CookFood>(mfDispatcher, "Kitchen");
			bus.SubscribeByType(_kitchenQueue);

			var waiter = new Waiter(bus);

			var factory = new MidgetFactory (bus);

			var house = new MidgetHouse(bus, factory);
            bus.SubscribeByType(house);

            _concurrentQueue.Start();
            _concurrentQueue2.Start();
            _concurrentQueue3.Start();
            _assistantQueue.Start();
            _kitchenQueue.Start();
            _cashierQueue.Start();

            _theTimer.Start();

            var items = new[]
            {
                new Tuple<int, string>(2, "pizza"),
                new Tuple<int, string>(1, "ice cream"),
            };

            for (var i = 0; i < 100; i++)
            {
                var id = waiter.PlaceOrder(10, items.ToList());
                bus.SubscribeByCorrelationId(id, new Monitor());
            }

        }

        private static void _theTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
			System.Console.WriteLine ("========================================");
            System.Console.WriteLine(_kitchenQueue.GetStats());
            System.Console.WriteLine(_concurrentQueue.GetStats());
            System.Console.WriteLine(_concurrentQueue2.GetStats());
            System.Console.WriteLine(_concurrentQueue3.GetStats());
            System.Console.WriteLine(_assistantQueue.GetStats());
            System.Console.WriteLine(_cashierQueue.GetStats());
			System.Console.WriteLine ("========================================");
        }
    }
}
