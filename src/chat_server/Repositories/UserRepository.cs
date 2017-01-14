using System;
using System.Collections.Generic;
using chat_server.Model;
using chat_server.Repositories.Interfaces;

namespace chat_server.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByConnection(string connectionId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserByLogin(Login login)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
