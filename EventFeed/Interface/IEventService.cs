using basicShoppingCartMicroservice.EventFeed.Models;

namespace basicShoppingCartMicroservice.EventFeed.Interface;

public interface IEventService
{
    IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
    void Raise(string eventName, object content);
}