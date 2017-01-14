using chat_server.Model;
using System.Collections.Generic;

namespace chat_server.Repositories.Interfaces
{
    public interface IChatRoomRepository
    {

        ChatRoom AddChatRoom(ChatRoom room);
        void RemoveChatRoom(int id);
        /// <summary>
        /// Gets the actual object from database
        /// </summary>
        /// <param name="room"></param>
        ChatRoom GetChatRoomById(int id);
        List<ChatRoom> GetChatRoomsByUser(User user);
        void Update(ChatRoom room);
        
    }
}
