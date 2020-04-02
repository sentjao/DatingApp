using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatingApi.NET.Models;
using DatingApi.NET.Services;
using Newtonsoft.Json;

namespace DatingApi.NET.Data
{
    public interface ISeedData
    {
         void Seed();
    }
    public class SeedData : ISeedData
    {
        private readonly ISHA512 _encoder;
        private readonly DataContext _context;
        public  SeedData(ISHA512 encoder, DataContext context)
        {
            _context = context;
            _encoder = encoder;
        }
        public void Seed()
        {
            if (_context.Users.Any())
                return;
            var json = File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<User[]>(json).Select(x => ModifyPwd(x));
             _context.Users.AddRange(users);
             _context.SaveChanges();
        }

        private User ModifyPwd(User user)
        {
            var pwd = _encoder.Encrypt("password");
            user.PasswordHash = pwd.PasswordHash;
            user.PasswordSalt = pwd.PasswordSalt;
            user.UserName = user.UserName.ToLower();
            return user;
        }
    }
}