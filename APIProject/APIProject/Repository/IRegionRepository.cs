using APIProject.Models.Domain;

namespace APIProject.Repository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
