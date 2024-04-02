using Inflow.Api.Constants;
using Inflow.Domain.Intefaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inflow.Api.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public MailController() { }
        public MailController(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        [HttpPost("register")]
        public async Task<ActionResult> SendRegisterEmail(string receiverEmail, string? name)
        {
            string subject = EmailConfigurations.Subject;
            string emailBody = EmailConfigurations.RegisterBody.Replace("{recipientName}", name);

            await _emailSender.SendEmail(receiverEmail, subject, emailBody);

            return Ok();
        }
    }
}
