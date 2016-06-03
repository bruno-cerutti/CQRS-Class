namespace CQRS.Main
{
	public interface IHandle<TMessage>
  	{	
		void Handle(TMessage message);
  	}
}