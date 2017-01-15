using System.Linq;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Repositories
{
    public class LoginRepository : GenericRepository<Login>, ILoginRepository 
    {
        public LoginRepository() : base()
        {
            
        }
        
        public bool CheckLogin(Login login)
        {
            return GetAll().Any(x => x.Username == login.Username);
        }

        public Login GetLogin(Login login)
        {
            return GetAll().Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
        }
    }
}
