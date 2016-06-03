using System;

namespace CQRS.Main
{
	public class OrderPlaced : AMessage
	{
		public Order Order { get; private set;}

		public OrderPlaced (Guid id, Guid correlationId, Guid causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

