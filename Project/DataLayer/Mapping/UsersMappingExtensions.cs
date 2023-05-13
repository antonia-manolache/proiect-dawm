using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mapping
{
    public static class UsersMappingExtensions
    {

        public static List<UserDto> ToUserDtos(this List<User> users)
        {
            var results = users.Select(e => e.ToUserDto()).ToList();

            return results;
        }

        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            var result = new UserDto();
            result.Id = user.Id;
            result.Username = user.Username;
            result.Recipes = user.Recipes.ToRecipeDtos();

            return result;
        }
    }
}