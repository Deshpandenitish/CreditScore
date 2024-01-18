using CreditLibrary.BL;
using CreditScore.Models.Entities;
using CreditScore.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace CreditLibrary.DAL
{
    public class NotificationRepo : INotification
    {
        readonly CreditScoreContext db;
        private readonly IAudit audit;
        public NotificationRepo(CreditScoreContext db, IAudit audit)
        {
            this.db = db;
            this.audit = audit;
        }
        public async Task<bool> addnotification(Notification nf)
        {
            try
            {
                var entity=db.Notifications.Add(nf);
                var res = await db.SaveChangesAsync();

                var auditLog = new AuditTrail
                {
                    UserId = entity.Entity.UserId,
                    ActivityDescription = "User Notification Added",
                    Timestamp = DateTime.Now,
                    TableName = nameof(Notification),
                };
                await audit.addaudit(auditLog);

                return res > 0 ? true : false;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Notification?> GetNotification(int usrid, string ns)
        {
            try
            {
                var rec = await db.Notifications.Where(r => r.UserId == usrid && r.NoteStatus == ns).FirstOrDefaultAsync();
                if (rec != null)
                {
                    return rec;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Notification>> GetlistofNotifications(int usrid, string ns)
        {
            try
            {
                var rec = await db.Notifications.Where(r => r.UserId == usrid && r.NoteStatus == ns).ToListAsync();
                return rec;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> removenotification(int nfid)
        {
            try
            {
                var rec = await db.Notifications.Where(x => x.NotificationId == nfid).FirstOrDefaultAsync();
                var entity=db.Notifications.Remove(rec);
                var res = db.SaveChangesAsync();

                var auditLog = new AuditTrail
                {
                    UserId = entity.Entity.UserId,
                    ActivityDescription = "User Notification Removed",
                    Timestamp = DateTime.Now,
                    TableName = nameof(Notification),
                };
                await audit.addaudit(auditLog);
                return res != null ? true : false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Updatenotification(Notification nf)
        {
            try
            {
                var rec = await db.Notifications.Where(x => x.NotificationId == nf.NotificationId).FirstOrDefaultAsync();
                if (rec != null)
                {
                    rec.NotificationDate = DateTime.Now;
                    rec.NoteStatus = nf.NoteStatus;
                    rec.NotificationName = nf.NotificationName;
                    var entity = db.Notifications.Update(rec);
                    var res = db.SaveChangesAsync();

                    var auditLog = new AuditTrail
                    {
                        UserId = entity.Entity.UserId,
                        ActivityDescription = "User Notification Updated",
                        Timestamp = DateTime.Now,
                        TableName = nameof(Notification),
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
    }
}
