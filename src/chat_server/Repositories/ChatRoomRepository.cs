using chat_server.Model;
using chat_server.Repositories.Interfaces;
using System.Collections.Generic;
using System;

namespace chat_server.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        public ChatRoom AddChatRoom(ChatRoom room)
        {
            throw new NotImplementedException();
        }

        public ChatRoom GetChatRoomById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ChatRoom> GetChatRoomsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public void RemoveChatRoom(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ChatRoom room)
        {
            throw new NotImplementedException();
        }
    }
}
