using chat_server.Model;
using System.Collections.Generic;

namespace chat_server.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Gets user from database, identified by login
        /// </summary>
        /// <param name="login">Login with filled props</param>
        /// <returns>User from database</returns>
        User GetByLogin(Login login);

        /// <summary>
        /// Gets user from database, identified by name
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>List of users that have a name like required</returns>
        List<User> GetByName(string name);
    }
}
