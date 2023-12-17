using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVPrograms.Data;
using TVPrograms.Data.Repository;
using TVPrograms.Models.Events;

namespace TVPrograms.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly Repository _repository;
        public EventsController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet("ByChannelId/{channelId}")]
        public async Task<List<ResponseEvent>> ByChannelId(int channelId,int days=1)
        {
            return await _repository.ByChannelId(channelId, days);
        }

        [HttpGet("{eventId}")]
        public async Task<ResponseEvent> ById(Guid eventId)
        {
            return await _repository.ByEventId(eventId);
        }
    }
}
