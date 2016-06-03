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
		    IList<dynamic> handlers;
            if(!_subscriptions.TryGetValue(typeof(T).Name, out handlers))
                handlers = new List<dynamic>();

            handlers.Add(handler);
		}


	    public void Publish<TMessage>(TMessage message) where TMessage : AMessage
	    {
	        IList<dynamic> handlers;
	        if (_subscriptions.TryGetValue(typeof(TMessage).Name, out handlers))
	        {
	            foreach (var handler in handlers)
	            {
	                handler.Handle(message);
	            }
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

