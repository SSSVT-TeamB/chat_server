using System.Collections.Generic;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Hubs
{
    public class UserHub : ChatHub
    {

        public UserHub(IUserRepository userRepository) : base(userRepository)
        {
        }

        public List<User> GetContacts()
        {
            if (User == null)
                return null;

            return User.Contacts;
        }

        public List<User> FindContact(string name)
        {
            if (User == null)
                return null;
            
            return _userRepository.GetUsersByName(name);
        }

        public ActionResult AddContact(int userId)
        {   
            User user = _userRepository.GetUserById(userId);
            if (User == null || User.Contacts.Contains(user))
                return ActionResult.GENERAL_FAIL;

            if (User.Contacts == null)
                User.Contacts = new List<User>();

            User.Contacts.Add(user);
            user.Contacts.Add(User);

            foreach (string connectionId in user.Connections)
            {
                try{
                Clients.Client(connectionId).OnNewContactAdd(User);
                }
                catch{}
            }

            return ActionResult.SUCCESS;
        }
    }
}
