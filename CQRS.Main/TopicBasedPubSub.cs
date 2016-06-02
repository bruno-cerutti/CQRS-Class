using System;
using System.Collections.Generic;

namespace CQRS.Main
{
	public class TopicBasedPubSub : IPublisher
	{
		private readonly Dictionary<string, dynamic> _subscriptions;
	    private static object _lock = new object();

		public TopicBasedPubSub ()
		{
			_subscriptions = new Dictionary<string, dynamic> ();
		}

		#region IBUS implementation

		public void SubscribeByType<T> (IHandle<T> handler) where T : AMessage
		{
			_subscriptions [typeof(T).Name] = handler;
		}

	    public void Publish<TMessage>(TMessage message) where TMessage : AMessage
	    {
	        var handler = _subscriptions[typeof(TMessage).Name];
            handler.Handle(message);
        }

	    public void UnsubscribeByType<TMessage>(TMessage message)
	    {
	        lock (_lock)
	        {
                _subscriptions.Remove(typeof(TMessage).Name);
            }
        }

		#endregion

	}
}

