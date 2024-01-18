using CreditScore.Models.Entities;

namespace CreditScore.Models.Interface
{
    public interface IAudit
    {
        Task<bool> addaudit(AuditTrail aud);
        Task<List<AuditTrail>> listaudit(int usrid);
    }
}
