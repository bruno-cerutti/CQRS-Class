using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CQRS.Main
{
    public class Order
    {
        public int Id { get; }
        private readonly JObject _jsonOrder;

        public Order(string orderJson)
        {
            _jsonOrder = JObject.Parse(orderJson);

        }

		public bool DodgyCustomer {			
			get { return _jsonOrder ["dodgy"].Value<bool> (); }
			set { _jsonOrder ["dodgy"] = value; }
		}

        public Order(int id)
        {
            Id = id;
            _jsonOrder = new JObject {{"id", Id}};
        }

        public int TableNumber
        {
            get { return _jsonOrder["tableNumber"].Value<int>(); }
        }

        public decimal Tax
        {
            get { return _jsonOrder["tax"].Value<decimal>(); }
            set { _jsonOrder["tax"] = value; }
        }

        public decimal Total
        {
            get { return _jsonOrder["total"].Value<decimal>(); }
            set { _jsonOrder["total"] = value; }
        }

        public string Ingredients
        {
            get { return _jsonOrder["ingredients"].Value<string>(); }
            set { _jsonOrder["ingredients"] = value; }
        }

        public IEnumerable<LineItem> LineItems
        {
            get { return _jsonOrder["lineItems"].ToObject<IEnumerable<LineItem>>(); }
        }

        public bool Paid
        {
            get { return _jsonOrder["paid"].Value<bool>(); }
            set { _jsonOrder["paid"] = value; }
        }


        public override string ToString()
        {
            return _jsonOrder.ToString();
        }


        public void AddItem(int quantity, string item)
        {
            var lineItem = new LineItem
            {
                Quantity = quantity,
                Item = item
            };
            var array = _jsonOrder["lineItems"] as JArray;

            if(array == null)
                array = new JArray();

            array.Add(JObject.FromObject(lineItem));
            _jsonOrder["lineItems"] = array;
        }

        public string CookName
        {
            get { return _jsonOrder["cookName"].Value<string>(); }
			set { _jsonOrder["cookName"] = value; }
        }
    }

}
