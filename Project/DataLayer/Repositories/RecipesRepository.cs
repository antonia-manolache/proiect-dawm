using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataLayer.Repositories
{
    public class RecipesRepository : RepositoryBase<Recipe>
    {
        private readonly AppDbContext dbContext;

        public RecipesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Dictionary<RecipeType, List<Recipe>> GetGroupedRecipes()
        {
            var results = dbContext.Recipes
                .GroupBy(e => e.Type)
                .Select(e => new {Type = e.Key, Recipes = e.ToList() })
                .ToDictionary(e => e.Type, e => e.Recipes);

            return results;
        }
    }
}