namespace CQRS.Main.Messages
{
    public class SendToMeIn5 : AMessage
    {
        public AMessage Message { get; private set; }

        public SendToMeIn5(string id, string correlationId, string causeId, AMessage message) : base(id, correlationId, causeId)
        {
            Message = message;
        }
    }
}