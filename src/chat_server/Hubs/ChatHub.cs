using System.Threading.Tasks;
using chat_server.Model;
using chat_server.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace chat_server.Hubs
{
    public abstract class ChatHub : Hub
    {
        protected User User {get; set; }
        protected IUserRepository _userRepository;

        public ChatHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override Task OnConnected()
        {
            User = _userRepository.GetUserByConnection(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            User = _userRepository.GetUserByConnection(Context.ConnectionId);
            User.Connections.Remove(Context.ConnectionId);
            
            return base.OnDisconnected(stopCalled);
        }
    }
}
