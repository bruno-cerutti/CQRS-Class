using System;
using System.Collections.Generic;

namespace CQRS.Main
{
	public class TopicBasedPubSub : IPublisher
	{
		private readonly Dictionary<string, IList<dynamic>> _subscriptions;
	    private static object _lock = new object();

		public TopicBasedPubSub ()
		{
			_subscriptions = new Dictionary<string, IList<dynamic>> ();
		}

		#region IBUS implementation

		public void SubscribeByType<T> (IHandle<T> handler) where T : AMessage
		{
			_subscriptions [typeof(T).Name].Add(handler);
		}


	    public void Publish<TMessage>(TMessage message) where TMessage : AMessage
	    {
	        var handlers = _subscriptions[typeof(TMessage).Name];
	        foreach (var handler in handlers)
	        {
                handler.Handle(message);
            }
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

