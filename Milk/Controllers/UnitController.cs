using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
   
    public class UnitController : Controller
    {
        private readonly UnitProvider _unitProvider;
        public UnitController()
        {
            _unitProvider = new UnitProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _unitProvider.GetUnits();
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int unitId)
        {
            var result=_unitProvider.TryDeleteUnit(unitId, out var errorMessage);
            if (!result)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new UnitDto());
        }

        [HttpPost]
        public ActionResult Add(UnitDto unitDto)
        {
            if (!ModelState.IsValid)
                return View("Add", unitDto);

            _unitProvider.AddUnit(unitDto);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int unitId)
        {
            return View(_unitProvider.GetUnit(unitId));
        }

        [HttpPost]
        public ActionResult Edit(UnitDto unitDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", unitDto);

            _unitProvider.EditUnit(unitDto);
            return RedirectToAction("GetAll");
        }
    }
}