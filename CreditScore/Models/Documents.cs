using Amazon.S3.Transfer;
using Amazon.S3;
using Amazon;
using CreditScore.Models.Interface;

namespace CreditScore.Models
{
    public class Documents: IDocuments
    {
        public IConfiguration _configuration;
        // WebApplicationBuilder builder;
        public Documents(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task UploadFileToS3(IFormFile file)
        {
            var key = _configuration["AWS:AccessKeyId"];
            var key1 = _configuration["AWS:SecretKey"];
            using (var client = new AmazonS3Client(_configuration["AWS:AccessKeyId"], _configuration["AWS:SecretKey"], RegionEndpoint.USEast1)) {
                using (var newMemoryStream = new MemoryStream()) {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = "employeeverify",
                        CannedACL = S3CannedACL.PublicRead,
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }

    }
}
