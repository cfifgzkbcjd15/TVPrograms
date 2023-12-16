using Microsoft.AspNetCore.Mvc;
using TVPrograms.Code;
using TVPrograms.Data;
using TVPrograms.Data.Repository;
using TVPrograms.Models.Chats;

namespace TVPrograms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChanelsController : ControllerBase
    {
        private readonly Repository _repository;
        private readonly HttpClientCommand _clientCommand;
        public ChanelsController(Repository repository, HttpClientCommand clientCommand)
        {
            _repository = repository;
            _clientCommand = clientCommand;
        }
        [HttpPost]
        public async Task<List<Channel>> GetChannels(FilterChannels filters)
        {
            return await _repository.GetChannels(filters);
        }

        [HttpGet("ReplaceData")]
        public async Task ReplaceData()
        {
            var result = await _clientCommand.GetChannels();
            await _repository.AddChannelsToMailApi(result);
        }
        [HttpGet]
        public async Task ReplaceEvents()
        {
            _clientCommand.GetEvents(await _repository.GetAllChannels())
            await _repository.AddEvents();
        }
    }
}
