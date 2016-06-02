namespace CQRS.Main
{
    public interface IHandleOrder
    {
        void Handle(Order order);
    }
}