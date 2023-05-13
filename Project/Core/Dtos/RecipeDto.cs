using DataLayer.Enums;

namespace Core.Dtos
{
    public class RecipeDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public RecipeType Type { get; set; }
    }
}