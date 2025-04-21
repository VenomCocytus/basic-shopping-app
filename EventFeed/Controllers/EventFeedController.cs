using basicShoppingCartMicroservice.EventFeed.Interface;
using basicShoppingCartMicroservice.EventFeed.Models;
using Lombok.NET;
using Microsoft.AspNetCore.Mvc;

namespace basicShoppingCartMicroservice.EventFeed.Controllers;

[RequiredArgsConstructor]
[Route("/events")]
public partial class EventFeedController : ControllerBase
{
    private readonly IEventService _eventService;

    [HttpGet]
    public List<Event> GetEvents([FromQuery] long start, [FromQuery] long end)
    {
        return _eventService.GetEvents(start, end).ToList();
    }
}