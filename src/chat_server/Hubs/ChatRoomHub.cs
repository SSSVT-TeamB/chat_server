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

            return _chatRoomRepository.GetByUser(User);
        }

        /*
        * TODO merge CreateRoom and CreateRoomGroup
        */
        public ChatRoom CreateRoom(int partnerId)
        {
            
            User partner = _userRepository.GetById(partnerId);
            

            if (User == null || partner == null)
                return null;

            ChatRoom room = _chatRoomRepository.Add(new ChatRoom(null,User.Name+" - "+partner.Name));
            
            _chatRoomRepository.AddRoomMember(room, User);
            _chatRoomRepository.AddRoomMember(room, partner);

            List<string> partnerConnectionIds = GetConnectionIds(partner);
            
            if (partnerConnectionIds.Count == 0)
            {
            foreach (string connectionId in partnerConnectionIds)
            {
                try{
                    Clients.Client(connectionId).OnNewRoomAdd(room);
                }
                catch{}
            }
            }
            return room;
        }

        public ChatRoom CreateRoomGroup(int[] partnerIds)
        {
            List<User> partners = new List<User>();

            foreach(int partnerId in partnerIds)
            {
                User partner = _userRepository.GetById(partnerId);
                if (partner != null)
                    partners.Add(partner);
            }

            if (User == null || partners.Count == 0)
                return null;

            ChatRoom room = _chatRoomRepository.Add(new ChatRoom(User,"Group chat"));

            foreach (User user in partners)
            {
                _chatRoomRepository.AddRoomMember(room,user);
            }

            _chatRoomRepository.AddRoomMember(room,User);

            _chatRoomRepository.Update(room);

            foreach (User client in partners)
            {
                foreach (string connectionId in GetConnectionIds(client))
                {
                    try{
                    Clients.Client(connectionId).OnNewChatRoomAdd(room);
                    }
                    catch
                    {}
                }
            }

            return room;
        }

        /*
        *TODO MAKE A NEW ROOM WHEN PRIVATE_CHAT
        */
        public ActionResult AddPartner(int partnerId, int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetById(chatRoomId);
            User partner = _userRepository.GetById(partnerId);

            if (User == null || room == null || partner == null)
                return ActionResult.GENERAL_FAIL;

            if (room.Owner != User && room.Owner != null)
                return ActionResult.NOT_ENOUGH_PERMISSIONS;

            if (_chatRoomRepository.IsUserRoomMember(room, partner))
                return ActionResult.USER_EXISTS;
            
            if(room.Owner == null)
            room.Owner = User;

            _chatRoomRepository.AddRoomMember(room,partner);

            foreach (User client in _chatRoomRepository.GetMembersByRoom(room))
            {
                foreach (string connectionId in GetConnectionIds(client))
                {
                    try{
                    Clients.Client(connectionId).OnRoomPartnerAdd(room, partner);
                    }
                    catch
                    {}
                }
            }

            return ActionResult.SUCCESS;
        }

        public ActionResult RemoveRoom(int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetById(chatRoomId);

            if (User == null || room == null)
                return ActionResult.GENERAL_FAIL;

            if (room.Owner != User && room.Owner != null)
                return ActionResult.NOT_ENOUGH_PERMISSIONS;

            foreach (User client in _chatRoomRepository.GetMembersByRoom(room))
            {
                foreach (string connectionId in GetConnectionIds(client))
                {
                    try{
                    Clients.Client(connectionId).OnRoomRemove(room);
                    }
                    catch
                    {}
                }
            }
            
            _chatRoomRepository.RemoveRoomMembers(room);

            _chatRoomRepository.Remove(room.Id);

            return ActionResult.SUCCESS;
        }

        public ActionResult RenameRoom(string name, int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetById(chatRoomId);

            if (User == null || room == null)
                return ActionResult.GENERAL_FAIL;

            if (room.Owner != User && room.Owner != null || !_chatRoomRepository.IsUserRoomMember(room, User))
                return ActionResult.NOT_ENOUGH_PERMISSIONS;

            room.Name = name;

            _chatRoomRepository.Update(room);

            foreach (User client in _chatRoomRepository.GetMembersByRoom(room))
            {
                foreach (string connectionId in GetConnectionIds(client))
                {
                    try{
                    Clients.Client(connectionId).OnRoomRename(room);
                    }
                    catch
                    {}
                }
            }

            return ActionResult.SUCCESS;
        }

        public List<User> GetRoomMembers(int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetById(chatRoomId);

            if (User == null || room == null)
                return null;

            if (!_chatRoomRepository.IsUserRoomMember(room, User))
                return null;

            return _chatRoomRepository.GetMembersByRoom(room);
        }

    }
}
