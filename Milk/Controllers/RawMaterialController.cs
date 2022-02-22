using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
    public class RawMaterialController : Controller
    {
        private readonly RawMaterialProvider _rawMaterialProvider;
        public RawMaterialController()
        {
            _rawMaterialProvider = new RawMaterialProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _rawMaterialProvider.GetRawMaterials();
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int rawMaterialId)
        {
            var result=_rawMaterialProvider.TryDeleteRawMaterial(rawMaterialId,out var errorMessage);
            if (!result)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new RawMaterialDto());
        }

        [HttpPost]
        public ActionResult Add(RawMaterialDto rawMaterialDto)
        {
            if (!ModelState.IsValid)
                return View("Add", rawMaterialDto);

            var result = _rawMaterialProvider.TryAddRawMaterial(rawMaterialDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Add", rawMaterialDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int rawMaterialId)
        {
            return View(_rawMaterialProvider.GetRawMaterial(rawMaterialId));
        }

        [HttpPost]
        public ActionResult Edit(RawMaterialDto rawMaterialDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", rawMaterialDto);

            var result = _rawMaterialProvider.TryEditRawMaterial(rawMaterialDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Edit", rawMaterialDto);
            }

            return RedirectToAction("GetAll");
        }
    }
}