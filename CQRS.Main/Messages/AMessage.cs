using System;

namespace CQRS.Main
{
	public abstract class AMessage : Message
	{
		public Guid Id {
			get;
			private set;
		}
		
		public AMessage (Guid id)
		{
			Id = id;
		}
	}
}

