using System;
using System.Collections.Generic;
using System.Reflection;

namespace CQRS.Main
{
	public class TopicBasedPubSub : IPublisher
	{
		private readonly Dictionary<string, Action<object>> _subscriptions;

		public TopicBasedPubSub ()
		{
			_subscriptions = new Dictionary<string, Action<object>> ();
		}

		#region IBUS implementation

		public void Subscribe<T> (IHandle<T> handler)
		{
			_subscriptions [typeof(AMessage).Name] = FireHandler;
		}

		public void Publish<AMessage> (AMessage message)
		{
			_subscriptions [typeof(AMessage).Name](message);
		}

		private void FireHandler(object o){
			
		
		}


		#endregion

	}
}

