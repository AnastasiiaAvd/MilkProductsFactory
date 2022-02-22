using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeProvider _employeeProvider;

        public EmployeeController()
        {
            _employeeProvider = new EmployeeProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _employeeProvider.GetEmployees();
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int employeeId)
        {
            var result = _employeeProvider.TryDeleteEmployee(employeeId, out var errorMessage);

            if (!result)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new EmployeeDto());
        }

        [HttpPost]
        public ActionResult Add(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View("Add", employeeDto);

            _employeeProvider.AddEmployee(employeeDto);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int employeeId)
        {
            var data = _employeeProvider.GetEmployee(employeeId);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", employeeDto);

            _employeeProvider.EditEmployee(employeeDto);
            return RedirectToAction("GetAll");
        }
    }
}