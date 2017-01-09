using chat_server.Model;
using chat_server.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_server.Hubs
{
    public class MessagesHub : Hub
    {
        IMessageRepository _messageRepository;

        public MessagesHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public void SendMessage(Message message)
        {
            _messageRepository.AddMessage(message);
            Clients.All.OnReceiveMessage(message);
        }
    }
}
