using System.Collections.Generic;
using System.Linq;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public void Add(User owner, User contact)
        {
            Contact c = new Contact() {Owner = owner, User = contact};
            this.Add(c);
        }

        public List<User> GetByUser(User user)
        {
            return (from r in GetAll() where r.Owner == user select r.User).ToList();
        }
    }
}