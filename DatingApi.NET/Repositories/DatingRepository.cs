using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApi.NET.Data;
using DatingApi.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.NET.Repositories
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> FindUserAsync(int id)
        {
            return await _context.Users.Include(x=>x.Photos).SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IEnumerable<User>> FindUsersAsync()
        {
            return await _context.Users.Include(x=>x.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}