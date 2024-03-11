using CreditScore.Models.Interface;
using CreditScore.Models.Entities;

namespace CreditScore.Models
{
    public class Linqs : ILinq
    {
        private readonly IlinqRepo _repo;
        public Linqs(IlinqRepo repo)
        {
            _repo = repo;
        }
        public Document FindDocument(int Id)
        {
            return _repo.FindDocument(Id);
        }
        public Document First()
        {
            return _repo.First();
        }
        public Document Last()
        {
            return _repo.Last();
        }
        public Document FirstOrDefault()
        {
            return _repo.FirstOrDefault();
        }
        public Document LastOrDefault()
        {
            return _repo.LastOrDefault();
        }
        public List<Document> OrderBy()
        {
            return _repo.OrderBy();
        }
        public List<Document> OrderByDescending()
        {
            return _repo.OrderByDescending();
        }

    }
}
