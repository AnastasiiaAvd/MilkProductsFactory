using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
    public class BudgetController : Controller
    {
        private readonly BudgetProvider _budgetProvider;
        public BudgetController()
        {
            _budgetProvider = new BudgetProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _budgetProvider.GetBudgets();
            return View(data);
        }

        //[HttpGet]
        //public ActionResult Remove(int budgetId)
        //{
        //    _budgetProvider.DeleteBudget(budgetId);

        //    return RedirectToAction("GetAll");
        //}

        [HttpGet]
        public ActionResult PaySalary(int employeeId)
        {
            var result = _budgetProvider.TryPaySalary(employeeId, out var errorMessage);

            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("GetAll","Employee");
            }

            TempData["ErrorMessage"] = errorMessage;
            return RedirectToAction("GetAll", "Employee");
        }

        //[HttpGet]
        //public ActionResult Add()
        //{
        //    return View(new BudgetDto());
        //}

        //[HttpPost]
        //public ActionResult Add(BudgetDto budgetDto)
        //{
        //    _budgetProvider.AddBudget(budgetDto);
        //    return RedirectToAction("GetAll");
        //}

        [HttpGet]
        public ActionResult Edit(int budgetId)
        {
            return View(_budgetProvider.GetBuget(budgetId));
        }

        [HttpPost]
        public ActionResult Edit(BudgetDto budgetDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", budgetDto);

            var result = _budgetProvider.EditBudget(budgetDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Edit", budgetDto);
            }
            return RedirectToAction("GetAll");
        }
    }
}