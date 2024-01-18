using CreditLibrary.BL;
using Microsoft.AspNetCore.Mvc;

namespace CreditScoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        readonly ICreditScoreVal creditScoreVal;
        public CreditController(ICreditScoreVal creditScoreVal)
        {
            this.creditScoreVal = creditScoreVal;
        }

        [HttpPost("Addcreditscore")]
        public async Task<IActionResult> addcreditscore([FromBody] CreditScore.Models.Entities.CreditScore cs)
        {
            var res = await creditScoreVal.addcreditscore(cs);
            if (res)
            {
                return Ok(new { Message = "credit score added successfully" });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("deletecreditscore")]
        public async Task<IActionResult> Removecreditscore([FromQuery]int usrid)
        {
            var res = await creditScoreVal.removecreditscore(usrid);
            if (res)
            {
                return Ok(new { Message = "Credit Score removed successfully" });
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("updatecreditscore")]
        public async Task<IActionResult> Updatecreditscore([FromBody] CreditScore.Models.Entities.CreditScore cs)
        {
            var res = await creditScoreVal.Updatecreditscore(cs);
            if (res)
            {
                return Ok(new { Message = "Credit score updated successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Userid not found" });
            }
        }
        [HttpGet("getcreditscore")]
        public async Task<IActionResult> getcredscore([FromQuery]int usrid)
        {
            var res = await creditScoreVal.getcredscore(usrid);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(new { Message = "User id is not found" });
            }
        }
    }
}
