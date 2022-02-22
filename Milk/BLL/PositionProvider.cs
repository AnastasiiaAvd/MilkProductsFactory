using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Milk.BLL
{
    /// <summary>
    /// Провайдер для работы с должностями сотрудников
    /// </summary>
    public class PositionProvider
    {
        /// <summary>
        /// Получить должность по id
        /// </summary>
        public PossitionDto GetPosition(int id)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var position =dbContext.getPosition(id).FirstOrDefault();
                return new PossitionDto { PositionId = position.idPosition, PosotionName = position.posotionName };
            }
        }
        public void EditPosition(PossitionDto possitionDto)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                dbContext.updatePosition(possitionDto.PositionId, possitionDto.PosotionName);
            }
        }
        public List<PossitionDto> GetPositions()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var positions = dbContext.getALLPositions().Select(p => new PossitionDto
                {
                    PositionId = p.idPosition,
                        PosotionName = p.posotionName
                 }).ToList();

                return positions;
            }
        }
        public bool TryDeletePosition(int id, out string errorMessage)
        {
            errorMessage = null;
            using (var dbContext = new MilkProductsEntities3())
            {
                var position = dbContext.getPosition(id).FirstOrDefault();

                var employee = dbContext.Employees.FirstOrDefault(e => e.position == position.idPosition);

                if (employee != null)
                {
                    errorMessage =
                        $"Нельзя удалить должность '{position.posotionName}', так как ее использует сотрудник '{employee.emloyeeName}'";
                    return false;
                }

                dbContext.deletePosition(id);
                return true;
            }
        }

        public void AddPosition(PossitionDto position)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                dbContext.addPosition(position.PosotionName);
            }
        }
    }
}
