using System;

namespace CQRS.Main
{
	public class MidgetFactory
	{
		IPublisher _bus;
		public MidgetFactory (IPublisher bus)
		{
			_bus = bus;
		}


		public IMidget CreateMidget(Order order)
		{
			if (order.DodgyCustomer)
				return new DodgyMidget (_bus);
			return new Midget (_bus);
		}

	}
}

