using APIProject.Models.Domain;

namespace APIProject.Repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password); 
    }
}
