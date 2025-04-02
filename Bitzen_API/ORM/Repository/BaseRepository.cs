using Bitzen_API.ORM.Interface;

namespace Bitzen_API.ORM.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class

    {
        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
