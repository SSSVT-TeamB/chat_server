using System.Collections.Generic;
using System.Linq;
using chat_server.Model;
using chat_server.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace chat_server.Hubs
{
    public class ChatHub : Hub
    {
        protected User User 
        {
            get{
                User user;
                connectedUsers.TryGetValue(Context.ConnectionId, out user);
                return user;
            }
            set{
                connectedUsers.Add(Context.ConnectionId,value);
            }
        }

        static private Dictionary<string, User> connectedUsers = new Dictionary<string, User>();
        protected IUserRepository _userRepository;

        public ChatHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected List<string> GetConnectionIds(User user)
        {
            return connectedUsers.Where(x => x.Value == user).Select(r => r.Key).ToList();
        }
        protected List<string> GetConnectionIds()
        {
            return GetConnectionIds(User);
        }

    }
}