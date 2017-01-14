using System.Collections.Generic;
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

        public PermissionResult NewMessage(string text, int chatRoomId)
        {
            ChatRoom cr = _chatRoomRepository.GetChatRoomById(chatRoomId);
            if (User == null || cr == null || !cr.Clients.Contains(User))
                return PermissionResult.NOT_ENOUGH_PERMISSIONS;

            Message message = new Message(User, text);

            _messageRepository.AddMessage(message,cr);

            //notify all users in ChatRoom
            foreach (User user in cr.Clients)
            {
                foreach (string connectionId in user.Connections)
                {
                    try{
                        Clients.Client(connectionId).OnNewMessage(message,cr);
                    }
                    catch{}                
                }              
            }

            return PermissionResult.SUCCESS;
        }

        public List<Message> GetRoomMessages(int chatRoomId)
        {
            ChatRoom cr = _chatRoomRepository.GetChatRoomById(chatRoomId);
            if (User == null || cr == null || !cr.Clients.Contains(User))
                return null;

            return _messageRepository.GetMessagesByRoomId(cr.Id);
        }
    }
}
