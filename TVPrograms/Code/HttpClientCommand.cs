using Microsoft.AspNetCore.Mvc.RazorPages;
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
            var channels = new List<ChannelJson>();
            var uri = "https://tv.mail.ru/ajax/channel/list";
            while (true)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri + "?page=" + page++);
                var response = await _httpClient.SendAsync(request);
                var res = JsonConvert.DeserializeObject<ChannelsJson>(await response.Content.ReadAsStringAsync());
                channels.AddRange(res.MoreChannel);
                maxPage = res.MaxPage;
                if (page > maxPage)
                    break;
            }
            return channels;
        }
        public async Task<List<Event>> GetEvents(List<int> channels)
        {
            var events = new List<Event>();
            var currentDate = DateTime.Now;
            var uri = "https://tv.mail.ru/ajax/channel/?region_id=140&channel_type";
            foreach (var channel in channels)
            {
                for (var i = 0; i < 7; i++)
                {
                    try
                    {
                        //формат даты 2023-12-16
                        var date = currentDate.AddDays(i + 1).ToString("yyyy-MM-dd");
                        var request = new HttpRequestMessage(HttpMethod.Get, uri + "&channel_id=" + channel + "&date=" + date);
                        var response = await _httpClient.SendAsync(request);
                        var content = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<EventsJson>(content);
                        if (res.Form?.Date != null)
                        {
                            var eventDate = res.Form.Date.Value;
                            res.Schedule.ForEach(e => events.AddRange(e.Event.Current.Select(s => new Event
                            {
                                ChannelId = s.ChannelId,
                                Name = s.Name,
                                Title = s.Title,
                                StartDate = s.Start == null ? DateTime.MinValue : Convert.ToDateTime(eventDate + " " + s.Start),
                                CategoryId = s.CategoryId,
                                Id = Guid.NewGuid()
                            })));
                        }
                    }
                    catch
                    {
                        i -= 1;
                        //await Task.Delay(200);
                    }
                }
            };
            return events;
        }

    }
}
