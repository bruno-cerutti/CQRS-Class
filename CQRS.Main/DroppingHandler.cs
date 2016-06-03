using System;

namespace CQRS.Main
{
    public class DroppingHandler<T> : IHandle<T>
    {
        private readonly IHandle<T> _handler;

        public DroppingHandler(IHandle<T> handler)
        {
            _handler = handler;
        }

        public void Handle(T message)
        {
            var drop = GenRand(0, 1);
            if(drop > 0.1)
                _handler.Handle(message);
        }

        static double GenRand(double min, double max)
        {
            Random rand = new Random();
            return min + rand.NextDouble() * (max - min);
        }
    }
}