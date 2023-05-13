using Core.Dtos;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Enums;
using Infrastructure.Exceptions;
using System.Security.Claims;

namespace Core.Services
{
    public class RecipeService
    {
        private readonly UnitOfWork unitOfWork;

        public RecipeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public RecipeAddDto Add(RecipeAddDto payload)
        {
            if (payload == null) return null;

            var newRecipe = new Recipe
            {
                Title = payload.Title,
                Description = payload.Description,
                Type = payload.Type,
                UserId = payload.UserId
            };

            unitOfWork.Recipes.Insert(newRecipe);
            unitOfWork.SaveChanges();

            return payload;
        }

        public Dictionary<RecipeType, List<Recipe>> GetGroupedRecipes()
        {
            var results = unitOfWork.Recipes.GetGroupedRecipes();

            return results;
        }

        public bool EditDescription(RecipeEditDto payload)
        {
            if (payload == null || payload.Id == null)
            {
                return false;
            }

            var result = unitOfWork.Recipes.GetById(payload.Id);
            if (result == null) return false;

            result.Description = payload.Description;
            unitOfWork.SaveChanges();

            return true;
        }


        public bool DeleteRecipe(RecipeDeleteDto payload)
        {
            if (payload == null || payload.Id == null)
            {
                return false;
            }

            var existingRecipe = unitOfWork.Recipes.GetById(payload.Id);
            if (existingRecipe == null)
                throw new ResourceMissingException($"Recipe id {payload.Id} doesn't exist!");

            var result = unitOfWork.Recipes.GetById(payload.Id);

            unitOfWork.Recipes.Remove(result);
            unitOfWork.SaveChanges();

            return true;
        }
    }
}