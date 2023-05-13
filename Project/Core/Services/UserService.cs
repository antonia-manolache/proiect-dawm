using Core.Dtos;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Enums;
using DataLayer.Mapping;
using Infrastructure.Exceptions;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        private AuthorizationService authService { get; set; }

        public UserService(UnitOfWork unitOfWork, AuthorizationService authService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }

        public void Register(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return;
            }

            var existingUser = unitOfWork.Users.GetByUsername(registerData.Username);
            if (existingUser != null)
                throw new ForbiddenException($"Username {registerData.Username} taken!");

            var hashedPassword = authService.HashPassword(registerData.Password);
            var user = new User
            {
                Username = registerData.Username,
                PasswordHash = hashedPassword,
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();
        }
        public UserAddDto AddUser(UserAddDto payload)
        {
            if (payload == null) return null;

            var newUser = new User
            {
                Username = payload.Username,

            };

            unitOfWork.Users.Insert(newUser);
            unitOfWork.SaveChanges();

            return payload;
        }

        public List<User> GetAll()
        {
            var results = unitOfWork.Users.GetAll();

            return results;
        }

        public string Validate(LoginDto payload)
        {
            var user = unitOfWork.Users.GetByUsername(payload.Username);

            var passwordFine = authService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (passwordFine)
            {
                var role = GetRole(user);

                return authService.GetToken(user, role);
            }
            else
            {
                return null;
            }

        }

        public string GetRole(User user)
        {
            if (user.Username == "antom")
            {
                return "Admin";
            }
            else if (user.Username == "izal")
            {
                return "Admin";
            }
            else
            {
                return "User";
            }
        }
        public UserDto GetById(int userId)
        {
            var user = unitOfWork.Users.GetById(userId);

            var result = user.ToUserDto();

            return result;
        }

        public bool EditUsername(UserUpdateDto payload)
        {
            if (payload == null || payload.Username == null)
            {
                return false;
            }

            var result = unitOfWork.Users.GetById(payload.Id);
            if (result == null) return false;

            result.Username = payload.Username;
            unitOfWork.SaveChanges();

            return true;
        }

        public bool DeleteUser(UserDeleteDto payload)
        {
            if (payload == null)
            {
                return false;
            }

            var result = unitOfWork.Users.GetById(payload.Id);
      
            unitOfWork.Users.Remove(result);
            unitOfWork.SaveChanges();

            return true;
        }

        public RecipesByUser GetRecipesById(int userId)
        {
            var userWithRecipes = unitOfWork.Users.GetByIdWithRecipes(userId);

            var result = new RecipesByUser(userWithRecipes);

            return result;
        }
    }
}