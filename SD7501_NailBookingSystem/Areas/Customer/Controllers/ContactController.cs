using Microsoft.AspNetCore.Mvc;
using SD7501_NailBookingSystem.Models;
using SD7501_NailBookingSystem.ContactSupportService;
using MailKit;

namespace SD7501_NailBookingSystem.Areas.Customer.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IEmailService _emailSender;
        public ContactController(ILogger<ContactController> logger, IEmailService emailSender)
            {
                _logger = logger;
                _emailSender = emailSender;
            }

        [HttpGet]
        public IActionResult Contact()
        {
            var model = new EmailRequest
            {
                EmailTo = "pcforge.c@gmail.com"
            };
            return View(model);
        }

        [HttpPost("Contact")]
        public async Task<IActionResult> Contact(EmailRequest emailRequest)
        {
            try
            {
                await _emailSender.SendEmailAsync(emailRequest);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
