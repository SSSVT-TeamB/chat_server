using System;
using System.Collections.Generic;
using System.Linq;
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
            

            room.AddMember(User);
            room.AddMember(partner);

            _chatRoomRepository.Update(room);
            
            if (partner.Connections != null)
            {
            foreach (string connectionId in (from r in partner.Connections select r.ConnectionId))
            {
                try{
                    Clients.Client(connectionId).OnNewChatRoomAdd(room);
                }
                catch{}
            }
            }
            Console.WriteLine("---------------------"+room.Id+"-------------------------");
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
                room.AddMember(user);
            }

            room.AddMember(User);

            _chatRoomRepository.Update(room);

            foreach (User client in partners)
            {
                foreach (Connection connection in client.Connections)
                {
                    try{
                    Clients.Client(connection.ConnectionId).OnNewChatRoomAdd(room);
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

            if (room.Owner != User)
                return ActionResult.NOT_ENOUGH_PERMISSIONS;

            room.AddMember(partner);
            _chatRoomRepository.Update(room);

            return ActionResult.SUCCESS;
        }

        public ActionResult RemoveRoom(int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetById(chatRoomId);

            if (User == null || room == null)
                return ActionResult.GENERAL_FAIL;

            if (room.Owner == null)
                return ActionResult.PRIVATE_CHAT;

            if (room.Owner != User)
                return ActionResult.NOT_ENOUGH_PERMISSIONS;
            
            _chatRoomRepository.Remove(room.Id);

            return ActionResult.SUCCESS;
        }

        public ActionResult RenameRoom(string name, int chatRoomId)
        {
            ChatRoom room = _chatRoomRepository.GetById(chatRoomId);

            if (User == null || room == null)
                return ActionResult.GENERAL_FAIL;

            if (room.Owner == null)
                return ActionResult.PRIVATE_CHAT;

            if (room.Owner != User)
                return ActionResult.NOT_ENOUGH_PERMISSIONS;

            room.Name = name;

            _chatRoomRepository.Update(room);

            return ActionResult.SUCCESS;
        }

    }
}
