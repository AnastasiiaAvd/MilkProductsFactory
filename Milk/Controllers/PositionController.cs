using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
    public class PositionController : Controller
    {
        private readonly PositionProvider _positionProvider;
        public PositionController()
        {
            _positionProvider = new PositionProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _positionProvider.GetPositions();
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int positionId)
        {
            var result = _positionProvider.TryDeletePosition(positionId, out var errorMessage);
            
            if (!result)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new PossitionDto());
        }

        [HttpPost]
        public ActionResult Add(PossitionDto possitionDto)
        {
            if (!ModelState.IsValid)
                return View("Add", possitionDto);

            _positionProvider.AddPosition(possitionDto);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int positionId)
        {
            return View(_positionProvider.GetPosition(positionId));
        }

        [HttpPost]
        public ActionResult Edit(PossitionDto possitionDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", possitionDto);

            _positionProvider.EditPosition(possitionDto);
            return RedirectToAction("GetAll");
        }
    }
}