namespace CQRS.Main.Messages
{
    public class RetryCooking : AMessage
    {
        public Order Order { get; private set; }

        public RetryCooking(string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}