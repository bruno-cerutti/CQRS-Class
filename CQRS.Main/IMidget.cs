using System;

namespace CQRS.Main
{
	public delegate void ProcessTerminatedEvent(object sender, string s);

	public interface IMidget : IHandle<OrderPlaced>, IHandle<FoodCooked>, IHandle<OrderPriced>, IHandle<OrderPaid>, IHandle<Message>
	{
		event ProcessTerminatedEvent ProcessTerminated;

	}

}

