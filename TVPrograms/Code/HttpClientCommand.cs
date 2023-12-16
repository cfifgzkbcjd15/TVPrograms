using Newtonsoft.Json;
using TVPrograms.Data;
using TVPrograms.Models.Chats;

namespace TVPrograms.Code
{
    public class HttpClientCommand
    {
        private HttpClient _httpClient = new HttpClient();
        public async Task<List<ChannelJson>> GetChannels()
        {
            int page = 0;
            int maxPage = 0;
            var channels= new List<ChannelJson>();
            var uri = "https://tv.mail.ru/ajax/channel/list";
            while (true)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri + "?page=" + page++);
                var response=await _httpClient.SendAsync(request);
                var res= JsonConvert.DeserializeObject<ChannelsJson>(await response.Content.ReadAsStringAsync());
                channels.AddRange(res.MoreChannel);
                maxPage = res.MaxPage;
                if (page > maxPage)
                    break;
            }
            return channels;
        }
        public async Task<List<ChannelJson>> GetEvents(List<Channel> channels)
        {
            var events= new List<Event>();
            var uri = "https://tv.mail.ru/ajax/channel/list";
            while (true)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri + "?page=" + page++);
                var response=await _httpClient.SendAsync(request);
                var res= JsonConvert.DeserializeObject<ChannelsJson>(await response.Content.ReadAsStringAsync());
                channels.AddRange(res.MoreChannel);
                maxPage = res.MaxPage;
                if (page > maxPage)
                    break;
            }
            return channels;
        }

    }
}
