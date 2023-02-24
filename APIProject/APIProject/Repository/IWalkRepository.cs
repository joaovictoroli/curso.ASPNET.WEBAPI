using APIProject.Models.Domain;

namespace APIProject.Repository
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();

        Task<Walk> GetAsync(Guid Id);

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> UpdateAsync(Guid id, Walk walk);

        Task<Walk> DeleteAsync(Guid Id);
    }
}
