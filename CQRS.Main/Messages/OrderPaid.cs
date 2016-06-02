using System;

namespace CQRS.Main
{
	public class OrderPaid : AMessage
	{
		public Order Order {
			get;
			private set;
		}
		public OrderPaid (Guid id, Order order) : base(id)
		{
			Order = order;
		}
	}
}

