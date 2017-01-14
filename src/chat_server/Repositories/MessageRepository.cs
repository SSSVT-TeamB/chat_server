using chat_server.Model;
using chat_server.Repositories.Interfaces;
using System.Collections.Generic;
using System;

namespace chat_server.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public void AddMessage(Message message, ChatRoom room)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessagesByRoomId(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
