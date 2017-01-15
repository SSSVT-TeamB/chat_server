using chat_server.Model;
using System.Collections.Generic;

namespace chat_server.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        /// <summary>
        /// Adds message into database
        /// </summary>
        /// <param name="message"></param>
        /// <param name="room"></param>
        void Add(Message message, ChatRoom room);
        
        /// <summary>
        /// Returns list of messages for room
        /// </summary>
        /// <returns></returns>
        List<Message> GetByRoomId(int roomId);
    }
}
