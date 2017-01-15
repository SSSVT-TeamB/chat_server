using System;
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
            if (l == null)
                return null;

            User u = _userRepository.GetByLogin(l);

            //write connectionId to user object
            if (u.Connections == null)
                u.Connections = new List<Connection>();
            
            u.Connections.Add(new Connection(Context.ConnectionId));
            _userRepository.Update(u);

            User = u;

            return u;
        }

        public ActionResult Register(Login login, string username)
        {          
            try{
                if (_loginRepository.CheckLogin(login))
                    return ActionResult.USER_EXISTS;

                Login l = _loginRepository.Add(login);

                _userRepository.Add(new User(username,l));

                return ActionResult.SUCCESS;
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return ActionResult.GENERAL_FAIL;
            }
        }
    }
}
