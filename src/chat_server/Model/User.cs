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
        public List<string> Connections {get; set; }

        [JsonIgnoreAttribute]
        public virtual List<User> Contacts {get; set; }

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