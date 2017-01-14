using chat_server.Model;
using System.Collections.Generic;

namespace chat_server.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Adds message into database
        /// </summary>
        /// <param name="message"></param>
        /// <param name="room"></param>
        void AddMessage(Message message, ChatRoom room);
        
        /// <summary>
        /// Returns list of messages for room
        /// </summary>
        /// <returns></returns>
        List<Message> GetMessagesByRoomId(int roomId);
    }
}
