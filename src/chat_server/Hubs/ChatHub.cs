using chat_server.Model;
using chat_server.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace chat_server.Hubs
{
    public class ChatHub : Hub
    {
        static protected User User {get; set; }
        protected IUserRepository _userRepository;

        public ChatHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

    }
}