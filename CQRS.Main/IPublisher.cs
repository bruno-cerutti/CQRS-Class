using System;

namespace CQRS.Main
{
	public interface IPublisher
	{
		void SubscribeByType<TMessage>(IHandle<TMessage> handler) where TMessage : AMessage;
		void Publish<TMessage>(TMessage message) where TMessage : AMessage;
	    void UnsubscribeByType<TMessage>(TMessage message);
	}
}

