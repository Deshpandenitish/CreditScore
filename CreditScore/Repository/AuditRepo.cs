using CreditScore.Models.Entities;
using CreditScore.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace CreditLibrary.DAL
{
    public class AuditLogic : IAudit
    {
        readonly CreditScoreContext db;
        public AuditLogic(CreditScoreContext db)
        {
            this.db = db;
        }

        public async Task<bool> addaudit(AuditTrail aud)
        {
            try
            {
                db.AuditTrails.Add(aud);
                var res = await db.SaveChangesAsync();
                return res > 0 ? true : false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<AuditTrail>> listaudit( int usrid)
        {
            try
            {
                var rec = await (db.AuditTrails.Where(x => x.UserId == usrid).ToListAsync());
                return rec;
            }
            catch
            {
                throw;
            }
        }
    }
}
