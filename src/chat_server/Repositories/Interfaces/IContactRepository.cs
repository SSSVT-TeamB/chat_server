using System.Collections.Generic;
using chat_server.Model;

namespace chat_server.Repositories.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        /// <summary>
        /// Adds a contact to owner. Part of contact logic, unfortunately EF for .net core doesn't support it the right way
        /// </summary>
        /// <param name="owner">user that owns the contact</param>
        /// <param name="contact">shown user</param>
        void Add(User owner, User contact);

        /// <summary>
        /// Gets contacts of given user
        /// </summary>
        /// <param name="user">user that have the contacts you want to see</param>
        /// <returns>list of contacts (users)</returns>
        List<User> GetByUser(User user);
    }
}