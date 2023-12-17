namespace TVPrograms.Models.Chats
{
    public class RequestChat
    {
        public string Prompt { get; set; }
        public User User { get; set; }
    }
    public class User
    {
        public short Age { get; set; }
        public string Gender { get; set; }
        public string Geolocation { get; set; }
    }

}
