using System;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Main;

namespace Restaurant.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var printer = new Printer();
            var cashier = new Cashier(printer);
            var assistant = new AssistantManager(cashier);
            var concurrentQueueAssistant = new QueueHandler(assistant);
            var cook = new Cook(concurrentQueueAssistant) {Name = "John"};
            var cook2 = new Cook(concurrentQueueAssistant) { Name = "Jio" };
            var cook3 = new Cook(concurrentQueueAssistant) { Name = "Jack" };
            //var multiplexer = new Multiplexer(new []{cook, cook, cook});
            var concurrentQueue = new QueueHandler(cook);
            var concurrentQueue2 = new QueueHandler(cook2);
            var concurrentQueue3 = new QueueHandler(cook3);
            var dispatcher = new RRDispatcher(new[] { concurrentQueue, concurrentQueue2, concurrentQueue3 });
            var waiter = new Waiter(dispatcher);
            concurrentQueue.Start();
            concurrentQueue2.Start();
            concurrentQueue3.Start();
            concurrentQueueAssistant.Start();

            var items = new[]
            {
                new Tuple<int, string>(2, "pizza"),
                new Tuple<int, string>(1, "ice cream"),
            };

            //Parallel.For(1, 10, (i) => waiter.PlaceOrder(10, items.ToList()));
            for (int i = 0; i < 10; i++)
            {
                waiter.PlaceOrder(10, items.ToList());
            }
        }
    }
}
