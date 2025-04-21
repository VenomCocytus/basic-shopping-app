using basicShoppingCartMicroservice.EventFeed.Interface;
using basicShoppingCartMicroservice.EventFeed.Models;

namespace basicShoppingCartMicroservice.EventFeed.Impl;

public class EventService : IEventService
{
    public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
    {
        throw new NotImplementedException();
    }

    public void Raise(string eventName, object content)
    {
        throw new NotImplementedException();
    }
}