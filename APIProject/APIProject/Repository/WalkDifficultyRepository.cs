using APIProject.Data;
using APIProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Repository
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;
        public WalkDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await dbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await dbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await dbContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDifficulty != null)
            {
                dbContext.WalkDifficulty.Remove(existingWalkDifficulty);
                await dbContext.SaveChangesAsync();
                return existingWalkDifficulty;
            }

            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await dbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await dbContext.WalkDifficulty.FindAsync(id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await dbContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;
            await dbContext.SaveChangesAsync();
            return existingWalkDifficulty;  
        }
    }
}
