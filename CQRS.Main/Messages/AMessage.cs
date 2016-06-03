namespace CQRS.Main
{
	public abstract class AMessage : Message
	{
	    public string Id {
			get;
			private set;
		}

        public string CorrelationId { get; private set; }
        public string CauseId { get; private set; }

	    protected AMessage (string id, string correlationId, string causeId)
		{
            CauseId = causeId;
		    Id = id;
		    CorrelationId = correlationId;
		}
	}
}

