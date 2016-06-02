using System.IO;
using System.Linq;
using CQRS.Main;
using NUnit.Framework;

namespace CQRS.Test
{
    [TestFixture]
    public class OrderTests
    {
        private string _jsonDocument;

        [SetUp]
        public void Init()
        {
            //_jsonDocument = Encoding.UTF8.GetString(File.ReadAllBytes(@"c:\users\bruno.cerutti\documents\CQRS Class\CQRS.Test\Order.json"));
            _jsonDocument = new StreamReader(File.OpenRead(@"c:\users\bruno.cerutti\documents\CQRS Class\CQRS.Test\Order.json")).ReadToEnd();
        }

        [Test]
        public void CanReadTableNumber()
        {
            var order = new Order(_jsonDocument);

            Assert.AreEqual(23, order.TableNumber);
        }

        [Test]
        public void CanSerializeJson()
        {
            var order = new Order(_jsonDocument);

            Assert.AreEqual(_jsonDocument, order.ToString());
        }

        [Test]
        public void CanSetIngredients()
        {
            var order = new Order(_jsonDocument);

            order.Ingredients = "meat and bread";

            Assert.AreEqual("meat and bread", order.Ingredients);
        }

        [Test]
        public void CanReadLineItems()
        {
            var order = new Order(_jsonDocument);

            Assert.AreEqual(1 , order.LineItems.Count());
        }

        [Test]
        public void CanAddLineItem()
        {
            var order = new Order(_jsonDocument);
            order.AddItem(2, "pasta");

            Assert.AreEqual(2, order.LineItems.Count());
            Assert.AreEqual(2, order.LineItems.Last().Quantity);
            Assert.AreEqual("pasta", order.LineItems.Last().Item);
        }
    }
}