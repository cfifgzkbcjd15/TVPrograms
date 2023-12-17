using Microsoft.AspNetCore.Mvc;
using TVPrograms.Code;
using TVPrograms;
using TVPrograms.Data.Repository;
using TVPrograms.Models.Chats;
using TVPrograms.Data;
using TVPrograms.Services;
using TVPrograms.Models.Channels;
using Microsoft.AspNetCore.Authorization;

namespace TVPrograms.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private readonly Repository _repository;
        private readonly HttpClientCommand _clientCommand;
        public ChannelsController(Repository repository, HttpClientCommand clientCommand)
        {
            _repository = repository;
            _clientCommand = clientCommand;
        }
        /// <summary>
        /// Получение каналов
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ResponseChannel>> GetChannels(FilterChannels filters)
        {
            return await _repository.GetChannels(filters);
        }

        /// <summary>
        /// Рандомайзер
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public async Task AddRandomEvents()
        //{
        //    var result=await new RandomDataProgramms().GetRandomEvents(await _repository.GetChannelsId(),await _repository.CheckCountCategoryes());
        //    await _repository.AddEvents(result);
        //}

        //[HttpGet("ReplaceData")]
        //public async Task ReplaceData()
        //{
        //    var result = await _clientCommand.GetChannels();
        //    await _repository.AddChannelsToMailApi(result);
        //}
        //[HttpGet("ReplaceEvents")]
        //public async Task ReplaceEvents()
        //{
        //    var events = await _clientCommand.GetEvents(await _repository.GetChannelsId());
        //    await _repository.AddEvents(events);
        //}
    }
}
