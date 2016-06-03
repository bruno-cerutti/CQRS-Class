using System;

namespace CQRS.Main
{
	public class OrderPriced : AMessage
	{
		public Order Order {
			get;
			private set;
		}

		public OrderPriced (Guid id, Guid correlationId, Guid causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

