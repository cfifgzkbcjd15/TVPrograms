﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TVPrograms.Models.Chats
{
    public class ChannelsJson
    {
        [JsonProperty("channels")]
        public List<ChannelJson> MoreChannel { get; set; }
        [JsonProperty("total")]
        public int MaxPage { get; set; }
    }

    public class ChannelJson
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pic_url")]
        public string SmallImg { get; set; }

        [JsonProperty("pic_url_128")]
        public string LargeImg { get; set; }

        [JsonProperty("id")]
        public int ChannelId { get; set; }

        [JsonProperty("pic_url_64")]
        public string Img { get; set; }
    }

    public class Current
    {
        [JsonProperty("channel_id")]
        public int ChannelId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category_id")]
        public short CategoryId { get; set; }

        [JsonProperty("episode_title")]
        public string Title { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }
    }

    public class Date
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class EventJson
    {
        [JsonProperty("current")]
        public List<Current> Current { get; set; }
    }

    public class Form
    {
        [JsonProperty("date")]
        public Date Date { get; set; }
    }

    public class EventsJson
    {
        [JsonProperty("form")]
        public Form Form { get; set; }
        [JsonProperty("schedule")]
        public List<Schedule> Schedule { get; set; }
    }

    public class Schedule
    {
        [JsonProperty("event")]
        public EventJson Event { get; set; }
    }
}
