using DataLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class UserRecipesRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}