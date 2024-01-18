using CreditScore.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditScoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        readonly IAudit audit;
        public AuditController(IAudit audit)
        {
            this.audit = audit;
        }
        [HttpGet("getallaudits")]
        public async Task<IActionResult> Getlistofaudits([FromQuery] int usrid)
        {
            var res = await audit.listaudit(usrid);
            return res != null ? Ok(res) : NotFound();
        }
    }
}
