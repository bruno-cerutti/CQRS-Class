using System;

namespace CQRS.Main
{
	public class FoodCooked : AMessage
	{
		public Order Order {
			get;
			private set;
		}

		public FoodCooked (string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

