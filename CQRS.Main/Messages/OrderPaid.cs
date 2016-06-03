using System;

namespace CQRS.Main
{
	public class OrderPaid : AMessage
	{
		public Order Order {
			get;
			private set;
		}
		public OrderPaid (Guid id, Guid correlationId, Guid causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

