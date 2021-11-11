using AlkemyDisney.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.Services
{
    public class MailService : IMailService
    {

        private readonly ISendGridClient _sendGridClient;

        public MailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendEmail(User user)
        {
            var message = new SendGridMessage()
            {
                From = new EmailAddress("nicolas.ncordoba@gmail.com", "Alkemy Challenge Disney"),
                Subject = "Te registraste satisfactoriamente",
                PlainTextContent = $"Se creo tu usuario {user.UserName}"
            };

            message.AddTo(new EmailAddress(user.Email, user.UserName));

            await _sendGridClient.SendEmailAsync(message);
        }

    }
}
