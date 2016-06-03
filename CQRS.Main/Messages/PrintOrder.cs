namespace CQRS.Main.Messages
{
    public class PrintOrder : AMessage
    {
        public Order Order { get; private set; }

        public PrintOrder(string id, string correlationId, string causeId, Order order) : base(id, correlationId, causeId)
        {
            Order = order;
        }
    }
}