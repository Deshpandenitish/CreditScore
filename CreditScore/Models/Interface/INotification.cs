using CreditScore.Models.Entities;

namespace CreditLibrary.BL
{
    public interface INotification
    {
        Task<bool> addnotification(Notification nf);
        Task<Notification?> GetNotification(int usrid, string ns);

        Task<bool> removenotification(int nfid);
        Task<bool> Updatenotification(Notification nf);

        Task<List<Notification>> GetlistofNotifications(int usrid, string ns);
    }
}
