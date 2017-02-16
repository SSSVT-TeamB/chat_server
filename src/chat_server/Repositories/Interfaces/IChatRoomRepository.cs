using chat_server.Model;
using System.Collections.Generic;

namespace chat_server.Repositories.Interfaces
{
    public interface IChatRoomRepository : IGenericRepository<ChatRoom>
    {
        List<ChatRoom> GetByUser(User user);    
        void AddRoomMember(ChatRoom room, User newMember);  
        bool IsUserRoomMember(ChatRoom room, User member);
        List<User> GetMembersByRoom(ChatRoom room);
        void RemoveRoomMembers(ChatRoom room, List<User> members = null);
    }
}
