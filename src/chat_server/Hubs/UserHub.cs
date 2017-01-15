using System.Collections.Generic;
using System.Linq;
using chat_server.Model;
using chat_server.Repositories.Interfaces;


namespace chat_server.Hubs
{
    public class UserHub : ChatHub
    {
        public UserHub(IUserRepository userRepository) : base (userRepository)
        {

        }

        public List<User> GetContacts()
        {
            if (Clients.Caller.User == null)
                return null;

            return (List<User>)(from r in (List<Contact>)Clients.Caller.User.Contacts select r.User);
        }

        public List<User> FindContact(string name)
        {
            if (Clients.Caller.User == null)
                return null;
            
            return _userRepository.GetByName(name);
        }

        public ActionResult AddContact(int userId)
        {   
            User user = _userRepository.GetById(userId);
            if (Clients.Caller.User == null || (from r in (List<Contact>)Clients.Caller.User.Contacts select r.User).Contains(user))
                return ActionResult.GENERAL_FAIL;


            /*User.Contacts.User.Add(user);
            user.Contacts.User.Add(User);

            foreach (Connection connection in user.Connections)
            {
                try{
                Clients.Client(connection.ConnectionId).OnNewContactAdd(User);
                }
                catch{}
            }*/

            return ActionResult.SUCCESS;
        }
    }
}
