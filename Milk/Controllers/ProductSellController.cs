using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;

namespace Milk.Controllers
{
    public class ProductSellController : Controller
    {
        private readonly ProductSellProvider _productSellProvider;

        public ProductSellController()
        {
            _productSellProvider = new ProductSellProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _productSellProvider.GetProductSells();
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int productSellId)
        {
            var result = _productSellProvider.TryDeleteProductSell(productSellId, out var errorMessage);

            if (!result)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new ProductSellDto());
        }

        [HttpPost]
        public ActionResult Add(ProductSellDto productSellDto)
        {
            if (!ModelState.IsValid)
                return View("Add", productSellDto);
            var result = _productSellProvider.TryAddProductSell(productSellDto, out var errorMessage);

            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Add", productSellDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int productSellId)
        {
            var data = _productSellProvider.GetProductSell(productSellId);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(ProductSellDto productSellDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", productSellDto);

            var result = _productSellProvider.TryEditProductSell(productSellDto, out var errorMessage);
            if (!result)
            { TempData["ErrorMessage"] = errorMessage; 
                return RedirectToAction("Edit");
            }

            return RedirectToAction("GetAll");
        }
    }
}