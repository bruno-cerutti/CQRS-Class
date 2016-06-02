using System;

namespace CQRS.Main
{
	public class OrderPlaced : AMessage
	{
		public Order Order { get; private set;}

		public OrderPlaced (Guid id, Order order) : base(id)
		{
			Order = order;
		}
	}
}

