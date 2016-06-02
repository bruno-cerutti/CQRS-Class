using System;

namespace CQRS.Main
{
	public class OrderPriced : AMessage
	{
		public Order Order {
			get;
			private set;
		}

		public OrderPriced (Guid id, Order order) : base(id)
		{
			Order = order;
		}
	}
}

