using System.Collections.Generic;
using Newtonsoft.Json;

namespace chat_server.Model
{
    public class ChatRoom
    {
        public int Id { get; set; }
        [JsonIgnoreAttribute]
        public virtual List<ChatRoomMember> Members { get; set; }
        
        public User Owner { get; set; }
        public string Name { get; set; }

        [JsonIgnoreAttribute]
        public virtual List<Message> Messages {get;set;}

        public ChatRoom(User owner, string name)
        {
            Owner = owner;
            Name  = name;
        }
        public ChatRoom()
        {
            
        }

        public void AddMember(User member)
        {
            if (Members == null)
                Members = new List<ChatRoomMember>();

            Members.Add(new ChatRoomMember(this,member));
        }
    }
}