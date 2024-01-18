using CreditScore.Models.Entities;

namespace CreditLibrary.BL
{
    public interface IFinanceDetails
    {
        Task<bool> addFinancedetails(FinancialDetail fd);
        Task<bool> UpdateFinanceDetails(FinancialDetail fd);
        Task<FinancialDetail?> getFinancialdetails(int usrid);
        Task<bool>delfinancedetails(int usrid);
        Task<List<FinancialDetail>> getAllFinancialdetails();

    }
}
