using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private UserService userService { get; set; }


        public UsersController(UserService userService)
        {
            this.userService = userService;
        }


        [HttpPost("/register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDto payload)
        {
            userService.Register(payload);
            return Ok();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto payload)
        {
            var jwtToken = userService.Validate(payload);

            return Ok(new { token = jwtToken });
        }

        [HttpGet("test-auth")]
        public IActionResult TestLogin()
        {

            ClaimsPrincipal user = User;

            var result = "";

            foreach (var claim in user.Claims)
            {
                result += claim.Type + " : " + claim.Value + "\n";
            }



            var hasRole_user = user.IsInRole("User");
            var hasRole_admin = user.IsInRole("Admin");

            return Ok(result);
        }

        [HttpGet("user-only")]
        [Authorize(Roles = "User")]
        public ActionResult<string> HelloUsers()
        {
            return Ok("Hello Users!");
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> HelloAdmins()
        {
            return Ok("Hello admins!");
        }



        [HttpPost("/add")]
        public IActionResult Add(UserAddDto payload)
        {
            var result = userService.AddUser(payload);

            if (result == null)
            {
                return BadRequest("User cannot be added");
            }

            return Ok(result);
        }


        [HttpGet("/get-all")]
        public ActionResult<List<User>> GetAll()
        {
            var results = userService.GetAll();

            return Ok(results);
        }

        [HttpGet("/get/{userId}")]
        public ActionResult<User> GetById(int userId)
        {
            var result = userService.GetById(userId);

            if (result == null)
            {
                return BadRequest("User not fount");
            }

            return Ok(result);
        }

        [HttpPatch("edit-username")]
        public ActionResult<bool> GetById([FromBody] UserUpdateDto userUpdateModel)
        {
            var result = userService.EditUsername(userUpdateModel);

            if (!result)
            {
                return BadRequest("User could not be updated.");
            }

            return result;
        }

        [HttpDelete("delete-user")]
        public ActionResult<bool> GetById([FromBody] UserDeleteDto userDeleteModel)
        {
            var result = userService.DeleteUser(userDeleteModel);

            if (!result)
            {
                return BadRequest("User could not be deleted.");
            }

            return result;
        }

        [HttpPost("recipes-by-user")]
        public ActionResult<RecipesByUser> Get_Recipes_ByUserId([FromBody] UserRecipesRequest request)
        {
            var result = userService.GetRecipesById(request.UserId);
            return Ok(result);
        }

        
    }
}