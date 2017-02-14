using System.Collections.Generic;
using System.Linq;
using chat_server.Contexts;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        GenericRepository<Contact> contactRepository = new GenericRepository<Contact>();
        public UserRepository(ChatContext context) : base(context)
        {
            
        }

        public User GetByLogin(Login login)
        {
            return GetAll().Where(x => x.Login == login).FirstOrDefault();
        }

        public List<User> GetByName(string name)
        {
            return GetAll().Where(x => x.Name.Contains(name)).ToList();
        }

        public void AddContact(User owner, User contact)
        {
            Contact c = new Contact(){Owner = owner, User = contact};

            contactRepository.Add(c);
        }
    }
}
