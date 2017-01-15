using System.Collections.Generic;
using System.Linq;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Hubs
{
    public class MessageHub : ChatHub
    {
        IMessageRepository _messageRepository;
        IChatRoomRepository _chatRoomRepository;

        public MessageHub(IMessageRepository messageRepository, IChatRoomRepository chatRoomRepository, IUserRepository userRepository) : base(userRepository)
        {
            _messageRepository = messageRepository;
            _chatRoomRepository = chatRoomRepository;
        }

        public ActionResult NewMessage(string text, int chatRoomId)
        {
            ChatRoom cr = _chatRoomRepository.GetById(chatRoomId);
            if (User == null || cr == null || !cr.HasMember(User))
                return ActionResult.NOT_ENOUGH_PERMISSIONS;

            Message message = new Message(User, text);

            _messageRepository.Add(message,cr);

            //notify all users in ChatRoom
            foreach (User user in (from r in cr.Members select r.User).ToList())
            {
                foreach (Connection connection in user.Connections)
                {
                    try{
                        Clients.Client(connection.ConnectionId).OnNewMessage(message,cr);
                    }
                    catch{}                
                }              
            }

            return ActionResult.SUCCESS;
        }

        public List<Message> GetRoomMessages(int chatRoomId)
        {
            ChatRoom cr = _chatRoomRepository.GetById(chatRoomId);
            if (User == null || cr == null || !cr.HasMember(User))
                return null;

            return _messageRepository.GetByRoomId(cr.Id);
        }
    }
}
