namespace CreditLibrary.BL
{
    public interface ICreditScoreVal
    {
        Task<bool> addcreditscore(CreditScore.Models.Entities.CreditScore cs);

        Task<bool> removecreditscore(int usrid);
        Task<bool> Updatecreditscore(CreditScore.Models.Entities.CreditScore cs);
       
        Task<CreditScore.Models.Entities.CreditScore?> getcredscore(int usrid);
    }
}
