using System;
using System.Linq;
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
            var cook = new Cook(assistant);
            var waiter = new Waiter(cook);

            var items = new[]
            {
                new Tuple<int, string>(2, "pizza"),
            };
            waiter.PlaceOrder(10, items.ToList());
        }
    }
}
