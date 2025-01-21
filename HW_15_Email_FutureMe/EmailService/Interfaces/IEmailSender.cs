using EmailService.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        void SendHtmlEmail(Message message);
        Task SendEmailAsync(Message message);
        Task SendEmailWithFilesAsync(Message message);
    }
}
