﻿using chat_server.Model;
using chat_server.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using chat_server.Contexts;

namespace chat_server.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ChatContext context) : base(context)
        {
            
        }
        public void Add(Message message, ChatRoom room)
        {
            message.Room = room;
            this.Add(message);
            context.SaveChanges();
        }

        public List<Message> GetByRoomId(int roomId)
        {
            return GetAll().Where(x => x.Room.Id == roomId).ToList();
        }
    }
}
