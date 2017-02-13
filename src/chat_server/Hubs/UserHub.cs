using System.Collections.Generic;
using System.Linq;
using chat_server.Model;
using chat_server.Repositories.Interfaces;


namespace chat_server.Hubs
{
    public class UserHub : ChatHub
    {
        IContactRepository _contactRepository;
        public UserHub(IUserRepository userRepository, IContactRepository contactRepository) : base (userRepository)
        {
            _contactRepository = contactRepository;
        }

        public List<User> GetContacts()
        {
            if (User == null)
                return null;

            return _contactRepository.GetByUser(User);
        }

        public List<User> FindContact(string name)
        {
            if (User == null)
                return null;
            
            List<User> users = _userRepository.GetByName(name);
            users.Remove(User);
            return users;
        }

        public ActionResult AddContact(int userId)
        {   
            User user = _userRepository.GetById(userId);
            
            if (User == null || _contactRepository.GetByUser(User).Contains(user))
                return ActionResult.GENERAL_FAIL;

            _contactRepository.Add(User,user);
            _contactRepository.Add(user,User);          

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
