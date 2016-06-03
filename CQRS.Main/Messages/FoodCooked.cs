using System;

namespace CQRS.Main
{
	public class FoodCooked : AMessage
	{
		public Order Order {
			get;
			private set;
		}

		public FoodCooked (Guid id, Guid correlationId, Guid causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

