using chat_server.Model;
using System.Collections.Generic;

namespace chat_server.Repositories.Interfaces
{
    public interface IChatRoomRepository : IGenericRepository<ChatRoom>
    {
        List<ChatRoom> GetByUser(User user);      
    }
}
