using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApi.NET.Models;

namespace DatingApi.NET.Repositories
{
    public interface IDatingRepository
    {
         void Add<T> (T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAllAsync();
         Task<IEnumerable<User>> FindUsersAsync();
         Task<User> FindUserAsync(int id);
    }
}