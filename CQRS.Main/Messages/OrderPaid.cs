using System;

namespace CQRS.Main
{
	public class OrderPaid : AMessage
	{
		public Order Order {
			get;
			private set;
		}
		public OrderPaid (string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

