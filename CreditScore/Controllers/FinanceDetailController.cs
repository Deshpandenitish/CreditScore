using CreditLibrary.BL;
using CreditScore.Models.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CreditScoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "CreditScore")]
    public class FinanceDetailController : ControllerBase
    {
        readonly IFinanceDetails financeDetails;
        public FinanceDetailController(IFinanceDetails financeDetails)
        {
            this.financeDetails = financeDetails;
        }
        [HttpPost("addfinancedetails")]
        public async Task<IActionResult> addFinancedetails([FromBody]FinancialDetail fd)
        {
            var res = await financeDetails.addFinancedetails(fd);
            if (res)
            {
                return Ok(new { Message = "Finance Details Added" });
            }
            else
            {
                return BadRequest(new { Message = "Fd is invalid" });
            }
        }
        [HttpPut("Updatefinancedetails")]
        public async Task<IActionResult> Updatefinancedetails([FromBody] FinancialDetail fd)
        {
            var res = await financeDetails.UpdateFinanceDetails(fd);
            if (res)
            {
                return Ok(new { Message = "Finance Details Updated Successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Fd is invalid" });
            }
        }

        [HttpGet("getfinancedetails")]
        public async Task<IActionResult> Getuserdetails([FromQuery] int usrid)
        {
            var res = await financeDetails.getFinancialdetails(usrid);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(new { Message = "User id  is not valid" });
            }

        }
        [HttpDelete("DeleteFinancedetails")]
        public async Task<IActionResult> Delfinancedetails([FromQuery]int usrid)
        {
            var res = await financeDetails.delfinancedetails(usrid);
            if (res)
            {
                return Ok(new { Message = "FinanceDetails removed successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Usr details are not found" });
            }
        }
        [HttpGet("Getallfinancedetails")]
        public async Task<IActionResult> getallfinancedetails()
        {
            var res = await financeDetails.getAllFinancialdetails();
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
