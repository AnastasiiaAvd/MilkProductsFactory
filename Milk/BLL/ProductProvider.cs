
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Milk.DataModels;

namespace Milk.BLL
{
    public class ProductProvider
    {
        public ProductDto GetProduct(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var product = dbContext.Products.FirstOrDefault(p => p.idProduct == id);
                return new ProductDto
                {
                    ProductId = product.idProduct,
                    ProductName = product.productName,
                    UnitId = product.Units.idUnit,
                    UnitName = product.Units.unitName,
                    Amount = product.Amount,
                    Sum = product.Sum,
                    Cost=product.Cost
                };
            }
        }
        public bool TryEditProduct(ProductDto productDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (productDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                dbContext.updateProduct(productDto.ProductId, productDto.ProductName, productDto.UnitId, productDto.Amount,
                   productDto.Sum, productDto.Cost);
            }

            return true;
        }
        public List<ProductDto> GetProducts()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var products = dbContext.getAllProducts().Select(product => new ProductDto
                {
                   ProductId = product.idProduct,
                   ProductName = product.productName,
                   UnitId = product.Unit,
                   UnitName = product.unitName,
                   Amount = product.Amount,
                   Sum = product.Sum,
                   Cost = product.Cost
                }).ToList();
                return products;
            }
        }

        public bool TryDeleteProduct(int id, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                var product = dbContext.Products.FirstOrDefault(p => p.idProduct == id);

                var productSell = dbContext.productSells.FirstOrDefault(e => e.product == product.idProduct);

                var production = dbContext.Production.FirstOrDefault(e => e.product == product.idProduct);

                var ingredient = dbContext.Ingredients.FirstOrDefault(e => e.product == product.idProduct);

                if (productSell != null)
                {
                    errorMessage =
                        $"Нельзя удалить продукт '{product.productName}', так как он участвует в продаже продукции /*'{productSell.Products.productName}'*/";
                    return false;
                }
                if (production != null)
                {
                    errorMessage =
                        $"Нельзя удалить продукт '{product.productName}', так как он участвует в производстве продукции /*'{production.Products.productName}'*/";
                    return false;
                }
                if (ingredient != null)
                {
                    errorMessage =
                        $"Нельзя удалить продукт '{product.productName}', так как он используется в ингредиентах /*в  '{ingredient.Products.productName}'*/";
                    return false;
                }

                dbContext.deleteProduct(id);
                return true;
            }
        }

        public bool TryAddProduct(ProductDto productDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (productDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                dbContext.addProduct(productDto.ProductName, productDto.UnitId, productDto.Amount, productDto.Sum, productDto.Cost);
            }

            return true;
        }
    }
}