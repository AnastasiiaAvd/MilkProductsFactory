using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Milk.DataModels;

namespace Milk.BLL
{
    public class IngredientProvider
    {
        public IngredientDto GetIngredient(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var ingredient = dbContext.Ingredients.FirstOrDefault(p => p.idIngredient == id);
                return new IngredientDto
                {
                    IngredientId = ingredient.idIngredient,
                    ProductId = ingredient.Products.idProduct,
                    ProductName = ingredient.Products.productName,
                    RawId = ingredient.RawMaterials.idRaw,
                    RawName = ingredient.RawMaterials.rawName,
                    Amount = ingredient.amount
                };
            }
        }
        public bool TryEditIngredient(IngredientDto ingredientDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (ingredientDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }
                dbContext.updateIngredient(ingredientDto.IngredientId, ingredientDto.ProductId, ingredientDto.RawId, ingredientDto.Amount);
            }

            return true;
        }
        public List<IngredientDto> GetIngredients()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var ingredients = dbContext.getAllIngredients().Select(p => new IngredientDto
                {
                   IngredientId = p.idIngredient,
                   ProductId = p.product,
                   ProductName = p.productName,
                   RawId = p.rawMaterial,
                   RawName = p.rawName,
                   Amount = p.amount,
                   RawMaterialAmount = p.rawAmount
                }).ToList();
                return ingredients;
            }
        }

        public List<IngredientDto> GetIngredientsByProductId(int productId)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var ingredients = dbContext.Ingredients.ToList().Where(p=>p.product==productId).Select(p => new IngredientDto
                {
                    IngredientId = p.idIngredient,
                    ProductId = p.Products.idProduct,
                    ProductName = p.Products.productName,
                    RawId = p.RawMaterials.idRaw,
                    RawName = p.RawMaterials.rawName,
                    Amount = p.amount,
                    RawMaterialAmount = p.RawMaterials.amount
                }).ToList();
                return ingredients;
            }
        }

        public void DeleteIngredient(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                dbContext.deleteIngredient(id);
            }
        }

        public bool TryAddIngredient(IngredientDto ingredientDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (ingredientDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }

                dbContext.addIngredient(ingredientDto.ProductId, ingredientDto.RawId, ingredientDto.Amount); 
            }

            return true;
        }
    }
}