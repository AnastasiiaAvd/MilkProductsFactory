using System.Web.Mvc;
using Milk.BLL;

namespace Milk.Controllers
{
    public class RawPurchaseController : Controller
    {
        private readonly RawPurchaseProvider _rawPurchaseProvider;
        public RawPurchaseController()
        {
            _rawPurchaseProvider = new RawPurchaseProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _rawPurchaseProvider.GetRawPurchases();
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int rawPurchaseId)
        {
            _rawPurchaseProvider.DeleteRawPurchase(rawPurchaseId);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new RawPurchaseDto());
        }

        [HttpPost]
        public ActionResult Add(RawPurchaseDto rawPurchaseDto)
        {
            if (!ModelState.IsValid)
                return View("Add", rawPurchaseDto);

            var result = _rawPurchaseProvider.TryAddPurchase(rawPurchaseDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Add", rawPurchaseDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int rawPurchaseId)
        {
            return View(_rawPurchaseProvider.GetRawPurchase(rawPurchaseId));
        }

        [HttpPost]
        public ActionResult Edit(RawPurchaseDto rawPurchaseDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", rawPurchaseDto);

            var result = _rawPurchaseProvider.TryEditRawPurchase(rawPurchaseDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Edit", rawPurchaseDto);
            }

            return RedirectToAction("GetAll");
        }
    }
}