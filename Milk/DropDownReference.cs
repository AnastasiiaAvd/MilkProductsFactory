using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Milk
{
    public class DropDownReference
    {
        public IEnumerable<SelectListItem> GetProduct
        {
            get
            {
                using (var dbContext = new MilkProductsEntities3())
                {
                    return dbContext.Products.ToList().Select(p => new SelectListItem
                    {
                        Value = p.idProduct.ToString(),
                        Text = p.productName
                    });
                }
            }
        }

        public IEnumerable<SelectListItem> GetEmployee
        {
            get
            {
                using (var dbContext = new MilkProductsEntities3())
                {
                    return dbContext.Employees.ToList().Select(p => new SelectListItem
                    {
                        Value = p.idEmployee.ToString(),
                        Text = p.emloyeeName
                    });
                }
            }
        }

        public IEnumerable<SelectListItem> GetRawMaterial
        {
            get
            {
                using (var dbContext = new MilkProductsEntities3())
                {
                    return dbContext.RawMaterials.ToList().Select(p => new SelectListItem
                    {
                        Value = p.idRaw.ToString(),
                        Text = p.rawName
                    });
                }
            }
        }

        public IEnumerable<SelectListItem> GetUnit
        {
            get
            {
                using (var dbContext = new MilkProductsEntities3())
                {
                    return dbContext.Units.ToList().Select(p => new SelectListItem
                    {Value = p.idUnit.ToString(), Text = p.unitName
                    });
                }
            }
        }
        public IEnumerable<SelectListItem> GetPositions
        {
            get
            {
                using (var dbContext = new MilkProductsEntities3())
                {
                    return dbContext.Positions.ToList().Select(p => new SelectListItem
                        {Value = p.idPosition.ToString(), Text = p.posotionName});
                }
            }
        }
        public IEnumerable<SelectListItem> GetCredits
        {
            get
            {
                using (var dbContext = new MilkProductsEntities3())
                {
                    return dbContext.Credit.ToList().Select(p => new SelectListItem
                    { Value = p.Id.ToString(), Text = p.Id.ToString() });
                }
            }
        }
    }
}


