using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{

    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeService recipeService;

        public RecipesController(RecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpPost("add")]
        public IActionResult Add(RecipeAddDto payload)
        {
            var result = recipeService.Add(payload);

            if (result == null)
            {
                return BadRequest("Recipe cannot be added");
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public ActionResult<List<RecipeViewDto>> GetAll()
        {
            var result = recipeService.GetGroupedRecipes();

            return Ok(result);
        }


        [HttpPatch("edit-description")]
        public ActionResult<bool> GetById([FromBody] RecipeEditDto recipeUpdateModel)
        {
            var result = recipeService.EditDescription(recipeUpdateModel);

            if (!result)
            {
                return BadRequest("Recipe could not be updated.");
            }

            return result;
        }

        [HttpDelete("delete-recipe")]
        public ActionResult<bool> GetById([FromBody] RecipeDeleteDto recipeDeleteModel)
        {
            var result = recipeService.DeleteRecipe(recipeDeleteModel);

            if (!result)
            {
                return BadRequest("Recipe could not be deleted.");
            }

            return result;
        }
    }
}