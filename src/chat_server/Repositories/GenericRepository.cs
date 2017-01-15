using System.Linq;
using chat_server.Contexts;
using chat_server.Factories;
using chat_server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chat_server.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ChatContext context = ChatContextFactory.Get();
        
        protected DbSet<T> dbset;

        public GenericRepository()
        {
            dbset = context.Set<T>();
        }

         public IQueryable<T> GetAll() 
        {
            return (IQueryable<T>) dbset;
        }

        public T Add(T entity)
        {
            dbset.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T GetById(int id)
        {
            return dbset.Find(id);
        }

        public void Remove(int id)
        {
            dbset.Remove(GetById(id));
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
