using chat_server.Model;
using chat_server.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using chat_server.Contexts;

namespace chat_server.Repositories
{
    public class ChatRoomRepository : GenericRepository<ChatRoom>, IChatRoomRepository
    {

        public ChatRoomRepository(ChatContext context) : base(context)
        {
            
        }
        public List<ChatRoom> GetByUser(User user)
        {
            return this.context.ChatRoomMembers.Where(x => x.User == user).Select(x => x.ChatRoom).ToList();
        }

        public void AddRoomMember(ChatRoom room, User newMember)
        {
            this.context.ChatRoomMembers.Add(new ChatRoomMember(room, newMember));
            this.context.SaveChanges();
        }

        public bool IsUserRoomMember(ChatRoom room, User member)
        {
            return this.context.ChatRoomMembers.Any(x => x.ChatRoom == room && x.User == member);
        }

        public List<User> GetMembersByRoom(ChatRoom room)
        {
            return this.context.ChatRoomMembers.Where(x => x.ChatRoom == room).Select(x => x.User).ToList();
        }
    }
}
