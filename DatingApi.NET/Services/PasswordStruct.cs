namespace DatingApi.NET.Services
{
    public class Password
    {
       public byte[] PasswordSalt{get; set;}
       public byte[] PasswordHash {get; set;}
    
    }
}