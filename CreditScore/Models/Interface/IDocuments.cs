namespace CreditScore.Models.Interface
{
    public interface IDocuments
    {
        Task UploadFileToS3(IFormFile file);
    }
}
