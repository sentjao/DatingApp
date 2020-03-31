using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace DatingApi.NET.Services
{
    public class SHA512 : ISHA512
    {
        public Password Encrypt(string value)
        {
            using (var sha512 = new HMACSHA512())
            {
                return new Password
                {
                    PasswordSalt = sha512.Key,
                    PasswordHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(value))
                };
            }
        }

        public bool Verify(Password password, string value)
        {
            using (var sha512 = new HMACSHA512(password.PasswordSalt))
            {
                var hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
                if (hash.Length != password.PasswordHash.Length)
                    return false;
                for (var i=0; i< hash.Length;i++)
                {
                    if (hash[i]!=password.PasswordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}