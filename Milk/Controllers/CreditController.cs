using Milk.BLL;
using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Milk.Controllers
{
    public class CreditController : Controller
    {
        private readonly CreditProvider creditProvider;
        public CreditController()
        {
            creditProvider = new CreditProvider();
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = creditProvider.GetCredits();
            return View(data);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new CreditDto());
        }
        [HttpPost]
        public ActionResult Add(CreditDto creditDto)
        {
            creditProvider.AddCredit(creditDto);

            return RedirectToAction("GetAll");
        }
    }
}