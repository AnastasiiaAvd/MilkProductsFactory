using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Milk.BLL
{
    public class UnitProvider
    {
        public UnitDto GetUnit(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var unit = dbContext.getUnit(id).FirstOrDefault();
                return new UnitDto { UnitId = unit.idUnit, UnitName = unit.unitName };
            }
        }
        public void EditUnit(UnitDto unitDto)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                dbContext.updateUnit(unitDto.UnitId, unitDto.UnitName);
            }
        }

        public List<UnitDto> GetUnits()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var units = dbContext.getAllUnits().Select(p => new UnitDto
                {
                    UnitId = p.idUnit,
                    UnitName = p.unitName
                }).ToList();
                return units;
            }
        }
        
        public bool TryDeleteUnit(int id, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext=new MilkProductsEntities3())
            {
                var unit = dbContext.Units.FirstOrDefault(p => p.idUnit == id);

                var product = dbContext.Products.FirstOrDefault(e => e.Unit == unit.idUnit);

                var rawMaterial = dbContext.RawMaterials.FirstOrDefault(e => e.idRaw == unit.idUnit);

                if (product != null)
                {
                    errorMessage =
                        $"Нельзя удалить единицу измерения '{unit.unitName}', так как она используется в описании продукции '{product.productName}'";
                    return false;
                }
                if (rawMaterial != null)
                {
                    errorMessage =
                        $"Нельзя удалить единицу измерения '{unit.unitName}', так как она используется в описании продукции '{product.productName}'";
                    return false;
                }

                dbContext.deleteUnit(id);
                return true;
            }
        } 

        public void AddUnit(UnitDto unit)
        {
            using (var dbContext=new MilkProductsEntities3())
            {
                dbContext.addUnit(unit.UnitName);
            }
        }
    }
}