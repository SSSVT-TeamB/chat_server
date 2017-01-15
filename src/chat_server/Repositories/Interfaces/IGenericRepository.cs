namespace chat_server.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T entity);
        void Remove(int id);
        T GetById(int id);
        void Update(T entity);
    }
}
