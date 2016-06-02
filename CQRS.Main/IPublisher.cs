using System;

namespace CQRS.Main
{
	public interface IPublisher
	{
		void Subscribe<TMessage>(IHandle<TMessage> handler);
		void Publish<TMessage>(TMessage message);
	}
}

