using System;
using CQRS.Main.Messages;

namespace CQRS.Main
{
	public delegate void ProcessTerminatedEvent(object sender, string s);

	public interface IMidget : IHandle<OrderPlaced>, 
        IHandle<FoodCooked>, 
        IHandle<OrderPriced>, 
        IHandle<OrderPaid>,
        IHandle<RetryCooking>, 
	    IHandle<Message>
	{
		event ProcessTerminatedEvent ProcessTerminated;

	}

}

