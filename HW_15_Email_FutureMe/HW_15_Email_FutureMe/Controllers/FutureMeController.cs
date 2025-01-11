using EmailService.Interfaces;
using EmailService.Models;
using HW_15_Email_FutureMe.Data;
using HW_15_Email_FutureMe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_15_Email_FutureMe.Controllers
{
    public class FutureMeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ApplicationContext _context;

        public FutureMeController(IEmailSender emailSender, ApplicationContext context)
        {
            _emailSender = emailSender;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendLetter(FutureLetter letter, string deliveryPeriod)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", letter);
            }

            letter.DeliveryDate = deliveryPeriod switch
            {
                "6m" => DateTime.Now.AddMonths(6),
                "1y" => DateTime.Now.AddYears(1),
                "3y" => DateTime.Now.AddYears(3),
                "today" => DateTime.Now,
                "5y" => DateTime.Now.AddYears(5),
                "10y" => DateTime.Now.AddYears(10),
                _ => DateTime.Now.AddMonths(6)
            };

            letter.IsSent = false;

            _context.FutureLetters.Add(letter);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Your letter has been scheduled! You will receive it on " +
                letter.DeliveryDate.ToString("d MMMM yyyy");

            return RedirectToAction(nameof(Index));
        }



        // Дейтсвие для отправки писем у которых дата отправки сегодня, Надо будет его вызывать каждый день потом для FutureMe
        // ps TODO: ежедневный вызов через сервис
        [HttpGet]
        public async Task<IActionResult> SendLettersForToday()
        {
            var lettersToSend = await _context.FutureLetters
                .Where(l => l.DeliveryDate.Date == DateTime.Now.Date && !l.IsSent)
                .ToListAsync();

            if (!lettersToSend.Any())
            {
                return Content("There are no emails to send today");
            }

            foreach (var letter in lettersToSend)
            {
                var message = new Message(
                    new string[] { letter.UserEmail },
                    "Your Future Letter", // Subject
                    letter.Content,
                    null); // файлы не добавлял в модель

                try
                {
                    await _emailSender.SendEmailAsync(message);
                    letter.IsSent = true;
                    letter.SentDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // TODO catch.....
                    return Content($"Error sending email: {ex.Message}");
                }
            }

            return Content("Letters have been sent!");
        }

    }
}
