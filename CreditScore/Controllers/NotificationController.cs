using CreditLibrary.BL;
using CreditScore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CreditScoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        readonly INotification notification;
        public NotificationController(INotification notification)
        {
            this.notification = notification;
        }
        [HttpPost("Addnotification")]
        public async Task<IActionResult> addnotification([FromBody] Notification nf)
        {
            var res = await notification.addnotification(nf);
            if (res)
            {
                return Ok(new { Message = "Notification added successfully" });

            }
            else
            {
                return BadRequest(new { Message = "nf invalid format" });
            }
        }
        [HttpGet("Getnotification")]
        public async Task<IActionResult> GetNotification([FromQuery] int usrid, [FromQuery] string ns)
        {
            var res = await notification.GetNotification(usrid, ns);
            if (res != null)
            {
                return Ok(res);

            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("Getallnotifications")]
        public async Task<IActionResult> GetlistofNotifications([FromQuery] int usrid, [FromQuery] string ns)
        {
            var res = await notification.GetlistofNotifications(usrid, ns);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("Deletenotification")]
        public async Task<IActionResult> removenotification([FromQuery]int nfid)
        {
            var res = await notification.removenotification(nfid);
            if (res)
            {
                return Ok(new { Message = "notifcation deleted successfully" });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("UpdateNotification")]
        public async Task<IActionResult> Updatenotification([FromBody]Notification nf)
        {
            var res = await notification.Updatenotification(nf);
            if (res)
            {
                return Ok(new { message = "Notification Updated Successfully" });
            }
            else
            {
                return BadRequest(new {Message="invalid userid and nfid"});
            }
        }
    }
}
