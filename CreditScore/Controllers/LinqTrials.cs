using CreditScore.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqTrials: ControllerBase
    {
        private readonly ILinq ilinq;
        public LinqTrials(ILinq ilinq)
        {
            this.ilinq= ilinq;
        }

        [HttpGet("Find/{id}")]
        public IActionResult Find(int id)
        {
            var result= ilinq.FindDocument(id);
            return result!=null && result.DocumentId>0 ? Ok() : NotFound();
        }

        [HttpGet("First")]
        public IActionResult First()
        {
            return Ok();
        }

        [HttpGet("Last")]
        public IActionResult Last() 
        {
            return Ok();
        }

        [HttpGet("OrderBy")]
        public IActionResult OrderBy()
        {
            return Ok();
        }

        [HttpGet("FirstOrDefault")]
        public IActionResult FirstOrDefault() { return Ok(); }

        [HttpGet("LastOrDefault")]
        public IActionResult LastOrDefault() { return Ok(); }

        [HttpGet("OrderByDescending")]
        public IActionResult OrderByDescending(int id)
        {
            return Ok();
        }

        [HttpGet("Min")]
        public IActionResult Min() { return Ok(); }

        [HttpGet("Max")]
        public IActionResult Max() { return Ok(); }

        [HttpGet("Count")]
        public IActionResult Count() { return Ok(); }

        [HttpGet("Sum")]
        public IActionResult Sum() { return Ok(); }

        [HttpGet("Distinct")]
        public IActionResult Distinct() { return Ok(); }

        [HttpGet("Average")]
        public IActionResult Average() { return Ok(); }

        [HttpGet("Join")]
        public IActionResult Join() { return Ok(); }

        [HttpGet("SelectAnonymous")]
        public IActionResult SelectAnonymous() { return Ok(); }
    }
}
