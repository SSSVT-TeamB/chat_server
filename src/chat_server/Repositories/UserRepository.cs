using System.Collections.Generic;
using System.Linq;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository() : base()
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
    }
}
