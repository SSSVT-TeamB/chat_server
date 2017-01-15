using Newtonsoft.Json;

namespace chat_server.Model
{
    public class ChatRoomMember
    {
        [JsonIgnoreAttribute]
        public int Id { get; set; }
        [JsonIgnoreAttribute]
        public ChatRoom ChatRoom { get; set; }
        public User User { get; set; }

        public ChatRoomMember(ChatRoom chatRoom, User user)
        {
            ChatRoom = chatRoom;
            User = user;
        }
        public ChatRoomMember()
        {
            
        }
    }
}