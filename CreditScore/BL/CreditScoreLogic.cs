using CreditLibrary.BL;
using CreditScore.Models.Entities;
using CreditScore.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace CreditLibrary.DAL
{
    public class CreditScoreLogic : ICreditScoreVal
    {
        readonly CreditScoreContext db;
        readonly IAudit audit;
        public CreditScoreLogic(CreditScoreContext db, IAudit audit)
        {
            this.db = db;
            this.audit = audit;
        }

        public static int creditscorepred(decimal dti)
        {
            decimal excellentDTILimit = 0.15m;
            decimal goodDTILimit = 0.25m;
            decimal fairDTILimit = 0.35m;
            Random random = new Random();
            if (dti <= excellentDTILimit)
            {
                var res = random.Next(800, 851);
                return res;
            }
            else if (dti <= goodDTILimit)
            {
                var res = random.Next(740, 800);
                return res;

            }
            else if (dti <= fairDTILimit)
            {
                var res = random.Next(580, 670);
                return res;

            }
            else
            {
                var res = random.Next(300, 580);
                return res;

            }

        }

        public async Task<bool> addcreditscore(CreditScore.Models.Entities.CreditScore cs)
        {
            try
            {
                var rec =await  (from fd in db.FinancialDetails where fd.UserId == cs.UserId select fd).FirstOrDefaultAsync();
                if (rec != null)
                {
                    decimal income = (decimal)rec.Income;
                    decimal expenses = (decimal)rec.Expenses;
                    decimal dti = (expenses / income);
                    var creditscore = creditscorepred(dti);
                    cs.CreditScore1 = creditscore;
                    cs.DebtToIncomeRatio = (decimal?)dti;
                    db.CreditScores.Add(cs);
                    var res = await db.SaveChangesAsync();
                    var auditLog = new AuditTrail
                    {
                        UserId = rec.UserId,
                        ActivityDescription = "User Credit score added",
                        Timestamp = DateTime.Now,
                        TableName = nameof(CreditScore),
                    };
                    await audit.addaudit(auditLog);
                    return res > 0 ? true : false;
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
        public async Task<bool> removecreditscore(int usrid)
        {
            try
            {
                var exec = await (db.CreditScores.Where(x => x.UserId == usrid).FirstOrDefaultAsync());
                var auditLog = new AuditTrail
                {
                    UserId = exec.UserId,
                    ActivityDescription = "User Creditscore removed",
                    Timestamp = DateTime.Now,
                    TableName = "Creditscore",
                };
                await audit.addaudit(auditLog);
                db.CreditScores.Remove(exec);
                var res = db.SaveChangesAsync();
                return res != null ? true : false;

            }
            catch
            {
                throw;
            }


        }

        public async Task<bool> Updatecreditscore(CreditScore.Models.Entities.CreditScore cs)
        {
            try
            {
                var exec = await (db.CreditScores.Where((x) => x.UserId == cs.UserId).FirstOrDefaultAsync());
                if (exec != null)
                {
                    exec.CreditScore1 = cs.CreditScore1;
                    exec.DebtToIncomeRatio = cs.DebtToIncomeRatio;
                    db.CreditScores.Update(exec);
                    db.SaveChangesAsync();
                    var auditLog = new AuditTrail
                    {
                        UserId = exec.UserId,
                        ActivityDescription = "User Creditscore updated",
                        Timestamp = DateTime.Now,
                        TableName = "Creditscore",
                    };
                    await audit.addaudit(auditLog);
                    return true;
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

        public async Task<CreditScore.Models.Entities.CreditScore?> getcredscore(int usrid)
        {
            try
            {
                var rec = await (db.CreditScores.Where(x => x.UserId == usrid).FirstOrDefaultAsync());
                var auditLog = new AuditTrail
                {
                    UserId = rec.UserId,
                    ActivityDescription = "User Creditscore retrived",
                    Timestamp = DateTime.Now,
                    TableName = "Creditscore",
                };
                await audit.addaudit(auditLog);
                return rec;
            }
            catch
            {
                throw;
            }
        }
    }
}
