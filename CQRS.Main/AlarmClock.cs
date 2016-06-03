using System;
using System.Collections.Generic;
using System.Timers;
using CQRS.Main.Messages;

namespace CQRS.Main
{
    public class AlarmClock : IHandle<SendToMeIn5>
    {
        private readonly IPublisher _bus;
        private Timer _timer;
        private readonly IDictionary<string, Tuple<DateTime,AMessage>> _delayedMessages;

        public AlarmClock(IPublisher bus)
        {
            _bus = bus;
            _timer = new Timer
            {
                AutoReset = true,
                Interval = 500
            };

            _timer.Elapsed += _timer_Elapsed;

            _timer.Start();
            _delayedMessages = new Dictionary<string, Tuple<DateTime, AMessage>>();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var messages = new List<AMessage>();

            var now = DateTime.Now.AddSeconds(-5);
            foreach (var delayedMessage in _delayedMessages)
            {
                if (delayedMessage.Value.Item1 < now)
                {
                    messages.Add(delayedMessage.Value.Item2);
                }
            }
            Console.WriteLine($"Got {messages.Count} messages to publish");
            foreach (var message in messages)
            {
                
                _bus.PublishByType(message);
                _delayedMessages.Remove(message.Id);
            }
        }

        public void Handle(SendToMeIn5 message)
        {
            _delayedMessages.Add(message.Message.Id, new Tuple<DateTime, AMessage>(DateTime.Now, message.Message));
        }
    }
}