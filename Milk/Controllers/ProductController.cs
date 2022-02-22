using System.Web.Mvc;
using Milk.BLL;
using Milk.DataModels;
using System.IO;
using ClosedXML.Excel;
using System;

namespace Milk.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductProvider _productProvider;

        public ProductController()
        {
            _productProvider = new ProductProvider();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _productProvider.GetProducts();
            return View(data);
        }

        [HttpGet]
        public ActionResult Remove(int productId)
        {
           var result= _productProvider.TryDeleteProduct(productId, out var errorMessage);
            if (!result)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new ProductDto());
        }

        [HttpPost]
        public ActionResult Add(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return View("Add", productDto);

            var result= _productProvider.TryAddProduct(productDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Add", productDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int productId)
        {
            var data = _productProvider.GetProduct(productId);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return View("Edit", productDto);

            var result = _productProvider.TryEditProduct(productDto, out var errorMessage);
            if (!result)
            {
                TempData["ErrorMessage"] = errorMessage;
                return View("Edit", productDto);
            }

            return RedirectToAction("GetAll");
        }

        public ActionResult Export()
        {
            var products = _productProvider.GetProducts();

            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Brands");

                worksheet.Cell(1,1).Value = "Id";
                worksheet.Cell(1,2).Value = "Product Name";
                worksheet.Cell(1,3).Value = "Unit Id";
                worksheet.Cell(1,4).Value = "Unit Name";
                worksheet.Cell(1,5).Value = "Amount";
                worksheet.Cell(1,6).Value = "Sum";
                worksheet.Cell(1,7).Value = "Cost";
                worksheet.Row(1).Style.Font.Bold = true;

                //нумерация строк/столбцов начинается с индекса 1 (не 0)
                for (int i = 0; i < products.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = products[i].ProductId;
                    worksheet.Cell(i + 2, 2).Value = products[i].ProductName;
                    worksheet.Cell(i + 2, 3).Value = products[i].UnitId;
                    worksheet.Cell(i + 2, 4).Value = products[i].UnitName;
                    worksheet.Cell(i + 2, 5).Value = products[i].Amount;
                    worksheet.Cell(i + 2, 6).Value = products[i].Sum;
                    worksheet.Cell(i + 2, 7).Value = products[i].Cost;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Products_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
    }

    }
}