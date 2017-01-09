using chat_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_server.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        List<Message> GetAll();
    }
}
