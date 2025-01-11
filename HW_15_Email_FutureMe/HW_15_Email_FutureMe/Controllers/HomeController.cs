using EmailService.Interfaces;
using EmailService.Models;
using HW_15_Email_FutureMe.Models;
using HW_15_Email_FutureMe.Util;
using HW_15_Email_FutureMe.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using System.Diagnostics;

namespace HW_15_Email_FutureMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;


        public HomeController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Regular
        [HttpGet]
        public IActionResult Email()
        {
            var message = new Message(new string[] { "730game@gmail.com" }, "Test regular email", "This is the content from our email.",null);  // null в конце тк добавил в Message еще файлы Attachments, но данное действие без них.
            _emailSender.SendEmail(message);
            return Content("Email отправлен!");
        }

        // HTML Content Email
        [HttpGet]
        public IActionResult HtmlEmail()
        {
            var message = new Message(new string[] { "730game@gmail.com" }, "Test html email", "<h2>This is the Html content from our email.</h2></br><p>test</p>",null);
            _emailSender.SendHtmlEmail(message);
            return Content("Html Email отправлен!");
        }

        // Async Email
        [HttpGet]
        public async Task<IActionResult> SendEmailAsync()   // /Home/SendEmail
        {
            var message = new Message(new string[] { "730game@gmail.com" }, "Test Async email", "This is the async content from our email.", null);
            await _emailSender.SendEmailAsync(message);
            return Content("Async Email отправлен!");
        }

        // Email With Files
        [HttpPost]
        public async Task<IActionResult> EmailWithFiles()
        {
            var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
            var message = new Message(new string[] { "730game@gmail.com" }, "Test Email With Files", "This is the Email With Files content from our email.", files);
            await _emailSender.SendEmailWithFilesAsync(message);
            return Content("Email отправлен!");
        }

        // Email Just With Files 
        //[HttpPost]
        //public IActionResult GetFiles()
        //{
        //    var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

        //    var message = new Message(new List<string> { "730game@gmail.com" }, "Test  GetFiles() email", "Thi is content from  GetFiles()", files);
        //    _emailSender.SendEmailWithFilesAsync(message);
        //    return Content("Sent");
        //}

        // Email Just With Files  v2 with View(),  Форма FutureMe в Index()
        [HttpPost]
        public async Task<IActionResult> SendEmailWithFiles(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new Message(new List<string> { model.Email }, model.Subject, model.Content, model.Attachments);
                await _emailSender.SendEmailWithFilesAsync(message);

                //return Content("The email has been successfully sent.");

                //string htmlContent = "<div class='alert alert-success' role='alert'>The email has been successfully sent.</div>";
                //return Content(htmlContent, "text/html");

                string url = Url.Action("Index", "Home");
                return new HtmlResult($"<div class='alert alert-success' role='alert'>The email has been successfully sent.  " +
                        $"<a href='{url}' class='alert-link'>Return...</a> </div>");

            }
            return View(model);
        }


    }
}
