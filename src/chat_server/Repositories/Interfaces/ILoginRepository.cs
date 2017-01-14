using chat_server.Model;

namespace chat_server.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        /// <summary>
        /// Adds login into database
        /// </summary>
        /// <param name="login">Login with filled props</param>
        /// <returns>Created instance from database</returns>
        Login AddLogin(Login login);

        /// <summary>
        /// Check if login exists
        /// </summary>
        /// <param name="login">Login with filled props</param>
        /// <returns>Login object recevied from database</returns>
        Login GetLogin(Login login);

        /// <summary>
        /// Check if login name doesn't exist in database
        /// </summary>
        /// <param name="login">Login with filled props</param>
        /// <returns>true if login is ok, false if fail</returns>
        bool CheckLogin(Login login);
    }
}
