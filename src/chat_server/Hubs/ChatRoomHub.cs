using System.Collections.Generic;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Hubs
{
    public class ChatRoomHub : ChatHub
    {
        IChatRoomRepository _chatRoomRepository;

        public ChatRoomHub(IChatRoomRepository chatRoomRepository,IUserRepository userRepository) : base(userRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public List<ChatRoom> GetUserRooms()
        {
            if (User == null)
                return null;

            return _chatRoomRepository.GetChatRoomsByUser(User);
        }

        public ActionResult CreateRoom(int partnerId)
        {
            User partner = _userRepository.GetUserById(partnerId);

            if (User == null || partner == null)
                return ActionResult.GENERAL_FAIL;
            
            List<User> clients = new List<User>();
            clients.Add(User);
            clients.Add(partner);

            ChatRoom room = _chatRoomRepository.AddChatRoom(new ChatRoom(clients,null,User.Name+" - "+partner.Name));

            foreach (string connectionId in partner.Connections)
            {
                try{
                    Clients.Client(connectionId).OnNewChatRoomAdd(room);
                }
                catch{}
            }

            return ActionResult.SUCCESS;
        }

        public ActionResult CreateRoom(List<int> partnerIds)
        {
            List<User> partners = new List<User>();

            foreach(int partnerId in partnerIds)
            {
                User partner = _userRepository.GetUserById(partnerId);
                if (partner != null)
                    partners.Add(partner);
            }

            if (User == null || partners.Count == 0)
                return ActionResult.GENERAL_FAIL;

            partners.Add(User);

            ChatRoom room = _chatRoomRepository.AddChatRoom(new ChatRoom(partners,User,"Group chat"));

            foreach (User client in partners)
            {
                foreach (string connectionId in client.Connections)
                {
                    try{
                    Clients.Client(connectionId).OnNewChatRoomAdd(room);
                    }
                    catch
                    {}
                }
            }

            return ActionResult.SUCCESS;
        }

        public PermissionResult AddPartner(int partnerId, int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetChatRoomById(chatRoomId);
            User partner = _userRepository.GetUserById(partnerId);

            if (User == null || room == null || partner == null)
                return PermissionResult.GENERAL_FAIL;

            if (room.Owner != User)
                return PermissionResult.NOT_ENOUGH_PERMISSIONS;

            room.Clients.Add(partner);
            _chatRoomRepository.Update(room);

            return PermissionResult.SUCCESS;
        }

        public RoomResult RemoveRoom(int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetChatRoomById(chatRoomId);

            if (User == null || room == null)
                return RoomResult.GENERAL_FAIL;

            if (room.Owner == null)
                return RoomResult.PRIVATE_CHAT;

            if (room.Owner != User)
                return RoomResult.NOT_ENOUGH_PERMISSIONS;
            
            _chatRoomRepository.RemoveChatRoom(room.Id);

            return RoomResult.SUCCESS;
        }

        public RoomResult RenameRoom(string name, int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetChatRoomById(chatRoomId);

            if (User == null || room == null)
                return RoomResult.GENERAL_FAIL;

            if (room.Owner == null)
                return RoomResult.PRIVATE_CHAT;

            if (room.Owner != User)
                return RoomResult.NOT_ENOUGH_PERMISSIONS;

            room.Name = name;

            _chatRoomRepository.Update(room);

            return RoomResult.SUCCESS;
        }

    }
}
