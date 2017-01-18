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
            if (User == null)
                return null;

            return (List<User>)(from r in User.Contacts select r.User);
        }

        public List<User> FindContact(string name)
        {
            if (User == null)
                return null;
            
            return _userRepository.GetByName(name);
        }

        public ActionResult AddContact(int userId)
        {   
            User user = _userRepository.GetById(userId);
            if (User == null || (from r in User.Contacts select r.User).Contains(user))
                return ActionResult.GENERAL_FAIL;

            User.Contacts.Add(new Contact() {Owner = User,User = user});
            user.Contacts.Add(new Contact() {Owner = user, User = User});

            foreach (string connectionId in GetConnectionIds(user))
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
