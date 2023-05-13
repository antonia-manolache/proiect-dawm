using DataLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class RecipeAddDto
    {
        [Required]
        public string Title { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required] 
        public RecipeType Type { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
