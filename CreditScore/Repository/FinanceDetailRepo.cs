using CreditLibrary.BL;
using CreditScore.Models.Entities;
using CreditScore.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace CreditLibrary.DAL
{
    public class FinanceDetailLogic : IFinanceDetails
    {
        readonly CreditScoreContext db;
        readonly IAudit audit;
        public FinanceDetailLogic(CreditScoreContext db, IAudit audit)
        {
            this.db = db;
            this.audit = audit;
        }
        public async Task<bool> addFinancedetails(FinancialDetail fd)
        {
            try
            {
                db.FinancialDetails.Add(fd);
                var res = await (db.SaveChangesAsync());
                var auditLog = new AuditTrail
                {
                    UserId = fd.UserId,
                    ActivityDescription = "User Finance Details added",
                    Timestamp = DateTime.Now,
                    TableName = "Financedetails",
                };
                await audit.addaudit(auditLog);
                return res > 0 ? true : false;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> UpdateFinanceDetails(FinancialDetail fd)
        {
            try
            {
                var rec = await (from details in db.FinancialDetails where details.UserId == fd.UserId select details).FirstOrDefaultAsync();
                if (rec != null)
                {
                    rec.Expenses = fd.Expenses;
                    rec.Income = fd.Income;
                    db.FinancialDetails.Update(rec);
                    var res = db.SaveChangesAsync();
                    var auditLog = new AuditTrail
                    {
                        UserId = fd.UserId,
                        ActivityDescription = "User Financedetails updated",
                        Timestamp = DateTime.Now,
                        TableName = "FinanceDetails",
                    };
                    await audit.addaudit(auditLog);
                    return res != null ? true : false;

                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<FinancialDetail?> getFinancialdetails(int usrid)
        {
            try
            {
                var rec = await (from details in db.FinancialDetails where details.UserId == usrid select details).FirstOrDefaultAsync();
                var auditLog = new AuditTrail
                {
                    UserId = rec.UserId,
                    ActivityDescription = "User Financedetails retrived by userid",
                    Timestamp = DateTime.Now,
                    TableName = "FinanceDetails",
                };
                await audit.addaudit(auditLog);
                return rec;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> delfinancedetails(int usrid)
        {
            try
            {
                var rec = await (from details in db.FinancialDetails where details.UserId == usrid select details).FirstOrDefaultAsync();

                db.FinancialDetails.Remove(rec);
                var res = db.SaveChangesAsync();

                var auditLog = new AuditTrail
                {
                    UserId = rec.UserId,
                    ActivityDescription = "User Financedetails deleted",
                    Timestamp = DateTime.Now,
                    TableName = "FinanceDetails",
                };
                await audit.addaudit(auditLog);
                return res != null ? true : false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<FinancialDetail>> getAllFinancialdetails()
        {
            try
            {
                return await db.FinancialDetails.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
