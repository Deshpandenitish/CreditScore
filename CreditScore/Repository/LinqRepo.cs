using CreditScore.Models.Entities;
using CreditScore.Models.Interface;

namespace CreditScore.Repository
{
    public class LinqRepo : IlinqRepo
    {
        public readonly CreditScoreContext _DBContext;
        public LinqRepo(CreditScoreContext dbContext)
        {
            _DBContext = dbContext;
        }

        public Document? FindDocument(int Id)
        {
            //var result = from s in _DBContext.Documents where s.DocumentId == Id select s;
            return _DBContext.Documents.Find(Id);
        }
        public Document First()
        {
            var result = (from s in _DBContext.Documents select s).First();
            return _DBContext.Documents.First();
        }
        public  Document Last()
        {
            var result = (from res in _DBContext.Documents select res).Last();
            return _DBContext.Documents.Last();
        }
        public Document FirstOrDefault()
        {
            var result = (from res in _DBContext.Documents select res).FirstOrDefault();
            return _DBContext.Documents.FirstOrDefault();
        }
        public Document LastOrDefault()
        {
            var result = (from res in _DBContext.Documents select res).LastOrDefault();
            return _DBContext.Documents.LastOrDefault();
        }
        public List<Document> OrderBy()
        {
            var result = (from res in _DBContext.Documents 
                          orderby res.CreatedDate 
                          ascending
                          select res);
            return _DBContext.Documents.OrderBy(e => e.CreatedDate).ToList();
        }
        public List<Document> OrderByDescending()
        {
            var result=(from res in _DBContext.Documents 
                        orderby res.CreatedDate 
                        descending 
                        select res).ToList();
            return _DBContext.Documents.OrderByDescending(e=>e.CreatedDate).ToList();
        }
    }
}
