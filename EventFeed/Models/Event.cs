namespace basicShoppingCartMicroservice.EventFeed.Models;

public record Event(
    long SequenceNumber,
    DateTimeOffset OccurrenceDate,
    string Name,
    object Content){}