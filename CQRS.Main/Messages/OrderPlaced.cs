using System;

namespace CQRS.Main
{
	public class OrderPlaced : AMessage
	{
		public Order Order { get; private set;}

		public OrderPlaced (string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
		{
			Order = order;
		}
	}
}

