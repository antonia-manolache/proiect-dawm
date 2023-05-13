using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Dtos;

public class RecipesByUser
{

    public int? UserId { get; set; }
    public string Username { get; set; }

    public List<RecipeDto> Recipes { get; set; } = new();

    public RecipesByUser(User user)
    {

        UserId = user?.Id;
        Username = user?.Username;

        if (user?.Recipes != null)
        {
            Recipes = user.Recipes
                .Select(g => new RecipeDto
                {
                    Title = g.Title,
                    Description = g.Description,
                    Type = g.Type
                })
                .ToList();
        }
    }

}