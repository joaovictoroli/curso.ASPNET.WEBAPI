using APIProject.Data;
using APIProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Repository
{
    //static user repository
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext dbContext;

        public UserRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await dbContext.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;
            return user;
        }
    }
}
