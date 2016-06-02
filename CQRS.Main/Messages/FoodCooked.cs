using System;

namespace CQRS.Main
{
	public class FoodCooked : AMessage
	{
		public Order Order {
			get;
			private set;
		}

		public FoodCooked (Guid id, Order order) : base(id)
		{
			Order = order;
		}
	}
}

