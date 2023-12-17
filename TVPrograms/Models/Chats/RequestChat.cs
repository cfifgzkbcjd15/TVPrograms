using TVPrograms.Models.Users;

namespace TVPrograms.Models.Chats
{
    public class RequestChat
    {
        public string Prompt { get; set; }
        public UserChat User { get; set; }
    }
    public class UserChat
    {
        public short Age { get; set; }
        public string Gender { get; set; }
        public string Geolocation { get; set; }
    }

}
