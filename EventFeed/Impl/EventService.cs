using basicShoppingCartMicroservice.EventFeed.Interface;
using basicShoppingCartMicroservice.EventFeed.Models;

namespace basicShoppingCartMicroservice.EventFeed.Impl;

public class EventService : IEventService
{

    private static long _currentSequenceNumber = 0;
    private static readonly IList<Event> Database = new List<Event>();
    
    public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber) => 
        Database.Where(element => 
                element.SequenceNumber == firstEventSequenceNumber && 
                element.SequenceNumber <= lastEventSequenceNumber)
            .OrderBy(element => element.SequenceNumber);
    

    public void Raise(string eventName, object content)
    {
        var sequenceNumber = Interlocked.Increment(ref _currentSequenceNumber);
        
        Database.Add(
            new Event(
                sequenceNumber,
                DateTimeOffset.UtcNow, 
                eventName,
                content));
    }
}