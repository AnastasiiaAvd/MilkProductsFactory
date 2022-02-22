using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Milk.DataModels;

namespace Milk.BLL
{
    public class RawMaterialProvider
    {
        /// <summary>
        /// Получить сырье по id
        /// </summary>
        public RawMaterialDto GetRawMaterial(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var rawMaterial = dbContext.RawMaterials.FirstOrDefault(p => p.idRaw ==id);
                return new RawMaterialDto
                {
                    RawId = rawMaterial.idRaw,
                    RawName = rawMaterial.rawName,
                    UnitId = rawMaterial.Units.idUnit,
                    UnitName = rawMaterial.Units.unitName,
                    Sum =rawMaterial.sum,
                    Amount = rawMaterial.amount,
                    Cost = rawMaterial.cost
                };
            }
        }
        public bool TryEditRawMaterial(RawMaterialDto rawMaterialDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (rawMaterialDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }
                if (rawMaterialDto.Sum <= 0)
                {
                    errorMessage = $"Недопустимое значение Стоимости.";
                    return false;
                }
                if (rawMaterialDto.Cost <= 0)
                {
                    errorMessage = $"Недопустимое значение Наценки.";
                    return false;
                }

                dbContext.updateRawMaterial(rawMaterialDto.RawId, rawMaterialDto.RawName, rawMaterialDto.UnitId,
                    rawMaterialDto.Sum, rawMaterialDto.Amount, rawMaterialDto.Cost);
            }

            return true;
        }
        public List<RawMaterialDto> GetRawMaterials()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var rawMaterials = dbContext.getAllRawMaterials().Select(rawMaterial => new RawMaterialDto
                {
                    RawId = rawMaterial.idRaw,
                    RawName = rawMaterial.rawName,
                    UnitId = rawMaterial.unit,
                    UnitName = rawMaterial.unitName,
                    Sum =rawMaterial.sum,
                    Amount =rawMaterial.amount,
                    Cost = rawMaterial.cost
                }).ToList();
                return rawMaterials;
            }
        }

        public bool TryDeleteRawMaterial(int id, out string errorMessage)
        {
            errorMessage = null; 
            using (var dbContext = new MilkProductsEntities3())
            {
                var rawMaterial = dbContext.RawMaterials.FirstOrDefault(p => p.idRaw == id);

                var ingredient = dbContext.Ingredients.FirstOrDefault(p => p.rawMaterial == rawMaterial.idRaw);

                var rawPurchase = dbContext.rawPurchases.FirstOrDefault(p => p.rawMaterial == rawMaterial.idRaw);

                if (ingredient != null)
                {
                    errorMessage =
                        $"Нельзя удалить сырье '{rawMaterial.rawName}', так как оно используется в качестве ингредиента для продукта '{ingredient.Products.productName}'";
                    return false;
                }
                if (rawPurchase != null)
                {
                    errorMessage =
                        $"Нельзя удалить сырье '{rawMaterial.rawName}', так как оно учавтует в закупке сырья";
                    return false;
                }

                dbContext.deleteRawMaterial(id);
                return true;
            }
        }

        public bool TryAddRawMaterial(RawMaterialDto rawMaterialDto, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                if (rawMaterialDto.Amount <= 0)
                {
                    errorMessage = $"Недопустимое значение Количества.";
                    return false;
                }
                if (rawMaterialDto.Sum <= 0)
                {
                    errorMessage = $"Недопустимое значение Стоимости.";
                    return false;
                }
                if (rawMaterialDto.Cost <= 0)
                {
                    errorMessage = $"Недопустимое значение Наценки.";
                    return false;
                }

                dbContext.addRawMaterial(rawMaterialDto.RawName, rawMaterialDto.UnitId,
                    rawMaterialDto.Sum, rawMaterialDto.Amount, rawMaterialDto.Cost);
            }
            return true;
        }
    }
}