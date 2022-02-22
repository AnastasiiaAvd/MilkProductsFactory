using Milk.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Milk.BLL
{
    public class CreditProvider
    {
        public List<CreditDto> GetCredits()
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var credits = dbContext.getAllCredits().Select(p => new CreditDto
                {
                    CreditId = p.Id,
                   CreditAmount=p.CreditAmount,
                   CreditRate=p.CreditRate,
                   Term=p.Term,
                   DateOfTaking=p.DateOfTaking
                }).ToList();
                return credits;
            }
        }
        public void AddCredit(CreditDto creditDto)
        {
            using (var dbContext = new MilkProductsEntities3())
            {
                var creditDomainModel = new Credit
                {
                    Id = creditDto.CreditId,
                    CreditAmount = creditDto.CreditAmount,
                    CreditRate = creditDto.CreditRate,
                    Term = creditDto.Term,
                    DateOfTaking = DateTime.Now
                };
                dbContext.Credit.Add(creditDomainModel);
                dbContext.SaveChanges();
            }
        }
    }
}