namespace DatingApi.NET.Services
{
    public interface ISHA512
    {
         Password Encrypt(string value);
         bool Verify(Password password, string value);
    }
}