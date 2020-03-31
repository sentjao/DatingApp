using System.Threading.Tasks;
using DatingApi.NET.Models;

namespace DatingApi.NET.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);   
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName); 
    }
}