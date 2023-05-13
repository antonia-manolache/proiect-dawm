using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;

        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetByIdWithRecipes(int userId)
        {
            var result = dbContext.Users
               .Select(e => new User
               {
                   Username = e.Username,
                   Id = e.Id,
                   Recipes = e.Recipes
                        .OrderByDescending(g => g.Title)
                        .ToList()
               })
               .FirstOrDefault(e => e.Id == userId);

            return result;
        }

        public User GetByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(s => s.Username == username);
        }




    }
}