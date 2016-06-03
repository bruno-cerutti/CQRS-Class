using System;

namespace CQRS.Main
{
	public class OrderPriced : AMessage
	{
		public Order Order {
			get;
			private set;
		}

		public OrderPriced (string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

