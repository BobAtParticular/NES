using System;
using System.Collections.Generic;
using System.Linq;

namespace NES
{
    public abstract class AggregateBase<T> : IEventSource where T : class
    {
        public Guid Id { get; protected set; }

        int IEventSource.Version
        {
            get { return _version; }
        }

        private int _version;
        private readonly List<T> _events = new List<T>();
        private static readonly IEventFactory<T> _eventFactory = new EventFactory<T>();
        private static readonly IEventHandlerFactory<T> _eventHandlerFactory = new EventHandlerFactory<T>();

        void IEventSource.Hydrate(IMemento memento)
        {
            Id = memento.Id;
            _version = memento.Version;

            RestoreSnapshot(memento);
        }

        void IEventSource.Hydrate(IEnumerable<object> events)
        {
            foreach (var @event in events.Cast<T>())
            {
                Raise(@event);
                _version++;
            }
        }

        IMemento IEventSource.TakeSnapshot()
        {
            var snapshot = TakeSnapshot();

            snapshot.Id = Id;
            snapshot.Version = _version;

            return snapshot;
        }

        IEnumerable<object> IEventSource.Flush()
        {
            var events = new List<T>(_events);

            _events.Clear();
            _version = _version + events.Count;

            return events;
        }

        protected void Apply<TEvent>(Action<TEvent> action) where TEvent : T
        {
            var @event = _eventFactory.Create(action);

            Raise(@event);

            _events.Add(@event);
        }

        protected virtual void RestoreSnapshot(IMemento memento)
        {
        }

        protected virtual IMemento TakeSnapshot()
        {
            return null;
        }

        private void Raise(T @event)
        {
            _eventHandlerFactory.Get(GetType(), @event.GetType())(this, @event);
        }
    }
}