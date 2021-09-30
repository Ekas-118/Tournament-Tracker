using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class EmailLogic
    {
        /// <summary>
        /// Configures the default email sender using app configuration values
        /// </summary>
        public static void ConfigureSender()
        {
            var sender = new SmtpSender(() => new SmtpClient());

            Email.DefaultSender = sender;
        }

        /// <summary>
        /// Sends an email to one person
        /// </summary>
        public static async Task SendEmail(string to, string subject, string body)
        {
            await SendEmail(new List<string> { to }, new List<string>(), subject, body);
        }

        /// <summary>
        /// Sends an email to a list of people
        /// </summary>
        public static async Task SendEmail(List<string> to, List<string> bcc, string subject, string body)
        {
            var toList = to.Select(x => new Address(x));
            var bccList = bcc.Select(x => new Address(x));

            var email = await Email
                .From(GlobalConfig.FromEmail())
                .To(toList)
                .BCC(bccList)
                .Subject(subject)
                .Body(body, isHtml: true)
                .SendAsync();
        }
    }
}
