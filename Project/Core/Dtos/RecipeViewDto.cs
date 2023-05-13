using DataLayer.Enums;

namespace Core.Dtos
{
    public class RecipeViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public RecipeType Type { get; set; }
    }
}