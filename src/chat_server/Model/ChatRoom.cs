using System.Collections.Generic;


namespace chat_server.Model
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public List<User> Clients { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }

        public ChatRoom(List<User> clients, User owner, string name)
        {
            Clients = clients;
            Owner = owner;
            Name  = name;
        }
        public ChatRoom()
        {
            
        }
    }
}