using chat_server.Model;
using System.Collections.Generic;

namespace chat_server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds user to database
        /// </summary>
        /// <param name="user">User with filled props, especially login</param>
        void AddUser(User user);

        /// <summary>
        /// Gets user from database, identified by login
        /// </summary>
        /// <param name="login">Login with filled props</param>
        /// <returns>User from database</returns>
        User GetUserByLogin(Login login);

        /// <summary>
        /// Gets user from database, identified by login
        /// </summary>
        /// <param name="connectionId">SignalR's connection id</param>
        /// <returns>User from database</returns>
        User GetUserByConnection(string connectionId);

        /// <summary>
        /// Gets user instance, indentified by id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User GetUserById(int userId);

        /// <summary>
        /// Gets user from database, identified by name
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>List of users that have a name like required</returns>
        List<User> GetUsersByName(string name);

        /// <summary>
        /// Sync object with the one in db
        /// </summary>
        /// <param name="user">User with changes to save</param>
        void Update(User user);
    }
}
