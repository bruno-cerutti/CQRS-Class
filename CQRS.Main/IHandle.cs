namespace CQRS.Main
{
	public interface IHandle<in TMessage>
    {
		void Handle(TMessage message);
    }
}