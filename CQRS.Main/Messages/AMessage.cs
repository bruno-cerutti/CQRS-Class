using System;

namespace CQRS.Main
{
	public abstract class AMessage : Message
	{
	    public Guid Id {
			get;
			private set;
		}

        public Guid CorrelationId { get; private set; }
        public Guid CauseId { get; private set; }

	    protected AMessage (Guid id, Guid correlationId, Guid causeId)
		{
            CauseId = causeId;
		    Id = id;
		    CorrelationId = correlationId;
		}
	}
}

