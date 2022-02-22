using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IngredientProvider _ingredientProvider;

        public IngredientController()
        {
            _ingredientProvider = new IngredientProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _ingredientProvider.GetIngredients();
            return View(data);
        }

        [HttpGet]
        public ActionResult GetIngredients(int productId)
        {
            var data = _ingredientProvider.GetIngredientsByProductId(productId);
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int ingredientId)
        {
            _ingredientProvider.DeleteIngredient(ingredientId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new IngredientDto());
        }

        [HttpPost]
        public ActionResult Add(IngredientDto ingredientDto)
        {
            if (!ModelState.IsValid)
                return View("Add", ingredientDto);

            var result= _ingredientProvider.TryAddIngredient(ingredientDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Add", ingredientDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int ingredientId)
        {
            var data = _ingredientProvider.GetIngredient(ingredientId);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(IngredientDto ingredientDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", ingredientDto);

            var result= _ingredientProvider.TryEditIngredient(ingredientDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Edit", ingredientDto);
            }
            
            return RedirectToAction("GetAll");
        }
    }
}