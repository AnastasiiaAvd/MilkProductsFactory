using Milk.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Milk.Controllers
{
    public class ExpensesHistoryController : Controller
    {
        private readonly ExpensesHistoryProvider _expensesHistoryProvider;
        public ExpensesHistoryController()
        {
            _expensesHistoryProvider = new ExpensesHistoryProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _expensesHistoryProvider.GetHistory();
            return View(data);
        }

        [HttpGet]
        public ActionResult GetAllPremiums()
        {
            var data = _expensesHistoryProvider.GetPremiums();
            return View(data);
        }

        [HttpGet]
        public ActionResult GetAllTaxes()
        {
            var data = _expensesHistoryProvider.GetTaxes();
            return View(data);
        }

        [HttpGet]
        public ActionResult PaySalary()
        {
            var result = _expensesHistoryProvider.PaySalary(out var errorMessage);

            if (!result)
                TempData["ErrorMessage"] = errorMessage;
            else
                TempData["ErrorMessage"] = "Операция выполнена.";

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Report()
        {
            var data = _expensesHistoryProvider.GetHistory();
            return View(data);
        }

        [HttpGet]
        public ActionResult PayTax()
        {
            var result = _expensesHistoryProvider.PayTax(out var errorMessage);

            if (!result)
                TempData["ErrorMessage"] = errorMessage;
            else
                TempData["ErrorMessage"] = "Операция выполнена.";

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult PayRent()
        {
            var result = _expensesHistoryProvider.PayRent(out var errorMessage);

            if (!result)
                TempData["ErrorMessage"] = errorMessage;
            else
                TempData["ErrorMessage"] = "Операция выполнена.";

            return RedirectToAction("GetAll");
        }
    }
}