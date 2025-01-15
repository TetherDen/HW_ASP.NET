using Blog.Interfaces;
using Blog.Models;
using EmailService.Interfaces;
using EmailService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class PublicationEmailNotificationRepository : IPublicationEmailNotification
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public PublicationEmailNotificationRepository(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task NotifySubscribersAsync(string authorId, string title, string description)
        {
            var subscribers = await _userManager.Users
                .Where(u => u.Subscriptions.Any(s => s.AuthorId == authorId))
                .Select(u => new { u.Email, u.Name }) 
                .ToListAsync();

            var author = await _userManager.FindByIdAsync(authorId);
            string authorName = author?.Name ?? "Unknown author";

            if (subscribers.Any())
            {
                var messageBody = subscribers.Select(u =>
                    $"Привет, {u.Name}<br /> " +
                    $"Новая публикация от: {authorName}<br />" +
                    $"Title: {title}<br />" +
                    $"Descipription: {description}")
                    .ToList();

                var message = new Message(
                    subscribers.Select(u => u.Email).ToList(),
                    "Blog.com New Publication notification",
                    string.Join("\n\n", messageBody),
                    null
                );

                await _emailSender.SendEmailAsync(message);
            }
        }

    }
}
