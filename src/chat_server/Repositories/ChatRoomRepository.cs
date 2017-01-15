using chat_server.Model;
using chat_server.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace chat_server.Repositories
{
    public class ChatRoomRepository : GenericRepository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository() : base()
        {
            
        }
        public List<ChatRoom> GetByUser(User user)
        {
            return GetAll().Where(x => x.HasMember(user)).ToList();
        }
    }
}
