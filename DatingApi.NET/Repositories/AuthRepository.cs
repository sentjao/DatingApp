using System;
using System.Threading.Tasks;
using DatingApi.NET.Data;
using DatingApi.NET.Models;
using DatingApi.NET.Services;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.NET.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _db;
        private readonly ISHA512 _sHA512;

        public AuthRepository(DataContext db, ISHA512 sHA512)
        {
            _sHA512 = sHA512;
            _db = db;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x=>x.UserName == userName.ToLower());
            if (user == null)
                return null;
            return VerifyPassword(user, password);
        }

        public async Task<User> Register(User user, string password)
        {
            var passwordHashSalt = _sHA512.Encrypt(password);
            var userContainer = new User
            {
               UserName = user.UserName.ToLower(),
               PasswordHash = passwordHashSalt.PasswordHash,
               PasswordSalt = passwordHashSalt.PasswordSalt
            };
            await _db.Users.AddAsync(userContainer);
            await _db.SaveChangesAsync();
            return userContainer;
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _db.Users.SingleOrDefaultAsync(x => x.UserName == userName.ToLower()) != null;
        }


        private User VerifyPassword(User user, string password)
        {
            var pwd = new Password
            {
                PasswordHash = user.PasswordHash, 
                PasswordSalt=user.PasswordSalt
            };
            if( _sHA512.Verify(pwd, password))
                return user;
            return null;
        }
    }
}