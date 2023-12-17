using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TVPrograms.Models.Chats;

namespace TVPrograms.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private string Uri = "https://824a-188-162-54-105.ngrok-free.app/suggest";

        /// <summary>
        /// Отправка сообщений ai
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("SendMessage")]
        public async Task<string> SendMessage(string message)
        {
            var age = Convert.ToInt16(User.FindFirst("Age").Value);
            var sex = User.FindFirst("Sex").Value;
            var ip = "2a03:d000:8605:a25:55ed:9a6b:f526:f6db";

            var request = new RequestChat() { Prompt = message, User = new User { Age = age, Gender = sex, Geolocation = ip } };
            var response = await new HttpClient().PostAsJsonAsync(Uri, request);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
