using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class UserAddDto
    {
        [Required, MaxLength(30)]
        public string Username { get; set; }

    }
}