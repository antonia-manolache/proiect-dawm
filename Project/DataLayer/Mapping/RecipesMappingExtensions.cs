using DataLayer.Dtos;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Mapping
{
    public static class RecipesMappingExtensions
    {
        public static List<RecipeDto> ToRecipeDtos(this List<Recipe> recipes)
        {
            if (recipes == null)
            {
                return null;
            }

            var results = recipes.Select(e => e.ToRecipeDto()).ToList();

            return results;
        }

        public static RecipeDto ToRecipeDto(this Recipe recipe)
        {
            if (recipe == null) return null;

            var result = new RecipeDto();
            result.Title = recipe.Title;
            result.Description = recipe.Description;
            result.Type = recipe.Type;

            return result;
        }
    }
}