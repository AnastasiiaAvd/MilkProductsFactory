using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
    public class ProductionController : Controller
        {
            private readonly ProductionProvider _productionProvider;

            public ProductionController()
            {
                _productionProvider = new ProductionProvider();
            }

            [HttpGet]
            public ActionResult GetAll()
            {
                var data = _productionProvider.GetProductions();
                return View(data);
            }

            [HttpGet]
            public ActionResult Remove(int productionId)
            {
                _productionProvider.DeleteProduction(productionId);

                return RedirectToAction("GetAll");
            }

            [HttpGet]
            public ActionResult Add()
            {
                return View(new ProductionDto());
            }

            [HttpPost]
            public ActionResult Add(ProductionDto productionDto)
            {
                if (!ModelState.IsValid)
                    return View("Add", productionDto);

                var result=_productionProvider.TryAddProduction(productionDto, out var errorMessage);
                if (!result)
                {
                    TempData["ErrorMessage"] = errorMessage;
                    return View("Add", productionDto);
                }

                return RedirectToAction("GetAll");
            }

            [HttpGet]
            public ActionResult Edit(int productionId)
            {
                var data = _productionProvider.GetProduction(productionId);
                return View(data);
            }

            [HttpPost]
            public ActionResult Edit(ProductionDto productionDto)
            {
                if (!ModelState.IsValid)
                    return View("Edit", productionDto);

                var result = _productionProvider.TryEditProduction(productionDto, out var errorMessage);
                if (!result)
                {
                    TempData["ErrorMessage"] = errorMessage;
                    return View("Edit", productionDto);
                }

                return RedirectToAction("GetAll");
            }
        }
    }