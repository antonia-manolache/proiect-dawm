using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class UserUpdateDto
    {
   
        public int Id { get; set; }

      
        [Required, MaxLength(30)]
        public string Username { get; set; }
    }
}