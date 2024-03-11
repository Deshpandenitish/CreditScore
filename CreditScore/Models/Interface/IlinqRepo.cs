using CreditScore.Models.Entities;

namespace CreditScore.Models.Interface
{
    public interface IlinqRepo
    {
        public Document? FindDocument(int Id);
        public Document First();
        public Document Last();
        public Document FirstOrDefault();
        public Document LastOrDefault();
        public List<Document> OrderBy();
        public List<Document> OrderByDescending();
    }
}
