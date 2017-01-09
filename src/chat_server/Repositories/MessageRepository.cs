using chat_server.Model;
using chat_server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_server.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private List<Message> _messages = new List<Message>();
        public void AddMessage(Message message)
        {
            _messages.Add(message);
        }

        public List<Message> GetAll()
        {
            return _messages;
        }
    }
}
