using Milk.BLL;
using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Milk.Controllers
{
    public class LoanPaymentController : Controller
    {
        private readonly LoanPaymentProvider loanPaymentProvider;
        public LoanPaymentController()
        {
            loanPaymentProvider = new LoanPaymentProvider();
        }

        [HttpGet]
        public ActionResult Report()
        {
            var data = loanPaymentProvider.GetLoanPayments();
            return View(data);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = loanPaymentProvider.GetLoanPayments();
            return View(data);
        }
        [HttpGet]
        public ActionResult AddLoanPayment()
        {
            return View(new LoanPaymentDto());
        }
        [HttpPost]
        public ActionResult AddLoanPayment(LoanPaymentDto loanPaymentDto)
        {
            var result = loanPaymentProvider.PayLoan(loanPaymentDto, out var errorMessage);

            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("AddLoanPayment", loanPaymentDto);
            }
            else
                TempData["ErrorMessage"] = "Операция выполнена.";

            return RedirectToAction("GetAll");
        }
        [HttpPost]
        public JsonResult GetLoansByCredit(int creditId)
        {
            var data = loanPaymentProvider.GetLoan(creditId);
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetPeny(PenyDto penyDto)
        {
            var data = loanPaymentProvider.CalculatePenalty(penyDto);
            return Json(data);
        }
    }

}