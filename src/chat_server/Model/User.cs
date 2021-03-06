using System.Collections.Generic;
using Newtonsoft.Json;

namespace chat_server.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnoreAttribute]
        public Login Login { get; set; }

        [JsonIgnoreAttribute]
        public virtual List<Contact> Contacts {get; set; }

        [JsonIgnoreAttribute]
        public virtual List<ChatRoomMember> MemberOf { get; set; }

        public User(string name, Login login)
        {
            Name = name;
            Login = login;
        }

        public User()
        {
            
        }
    }
}