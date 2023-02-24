using APIProject.Models.Domain;

namespace APIProject.Repository
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
