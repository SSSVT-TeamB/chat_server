using System.Collections.Generic;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Hubs
{
    public class LoginHub : ChatHub
    {
        ILoginRepository _loginRepository;


        public LoginHub(ILoginRepository loginRepository, IUserRepository userRepository) : base(userRepository)
        {
            _loginRepository = loginRepository;
        }

        public User Authentificate(Login login)
        {            
            Login l = _loginRepository.GetLogin(login);
            
            //if login doesn't exist in database, return null
            if (l != null)
                return null;

            User u = _userRepository.GetUserByLogin(l);

            //write connectionId to user object
            if (u.Connections == null)
                u.Connections = new List<string>();
            
            u.Connections.Add(Context.ConnectionId);
            _userRepository.Update(u);

            return u;
        }

        public RegisterResult Register(Login login, string username)
        {          
            try{
                if (!_loginRepository.CheckLogin(login))
                    return RegisterResult.USER_EXISTS;

                Login l = _loginRepository.AddLogin(login);

                _userRepository.AddUser(new User(username,l));

                return RegisterResult.SUCCESS;
            }
            catch{
                return RegisterResult.GENERAL_FAIL;
            }
        }
    }
}
