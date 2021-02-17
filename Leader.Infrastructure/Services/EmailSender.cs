using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Smeat.Leader.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task<Task> SendEmailAsync(string[] recipients, string subject, string body)
        {
            return await SendEmailAsync(recipients, subject, body, true, MailPriority.Normal, new string[] { });
        }
        public async Task<Task> SendEmailAsync(string[] recipients, string subject, string body, bool bodyIsHtml)
        {
            return await SendEmailAsync(recipients, subject, body, bodyIsHtml, MailPriority.Normal, new string[] { });
        }
        public async Task<Task> SendEmailAsync(string[] recipients, string subject, string body, MailPriority priority)
        {
            return await SendEmailAsync(recipients, subject, body, true, priority, new string[] { });
        }
        public async Task<Task> SendEmailAsync(string[] recipients, string subject, string body, string[] attachments)
        {
            return await SendEmailAsync(recipients, subject, body, true, MailPriority.Normal, attachments);
        }

        public async Task<Task> SendEmailAsync(string[] recipients, string subject, string body, bool bodyIsHtml, MailPriority priority, string[] attachments)
        {
            string smtpServer = "mail.loyaltylogistix.com";
            string smtpUsername = "autolog";
            string smtpPassword = "autologe01";
            string sender = "autolog@loyaltylogistix.com";

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = smtpServer;
                //smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;                
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                foreach (var recipient in recipients)
                {
                    var mailMessage = new MailMessage(sender, recipient, subject, body);
                    mailMessage.Priority = priority;
                    mailMessage.IsBodyHtml = bodyIsHtml;

                    if (attachments != null)
                    {
                        // Add the attachments    
                        foreach (string attachment in attachments)
                        {
                            mailMessage.Attachments.Add(new Attachment(attachment));
                        }
                    }
                    //smtpClient.SendAsync(mailMessage, string.Format("Email sent to {0}", recipient));
                    smtpClient.Send(mailMessage);
                }
            }
            return Task.CompletedTask;
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.    
            var token = (string)e.UserState;
            if (e.Cancelled)
            {
                //Console.WriteLine("Mail Cancelled");    
            }
            if (e.Error != null)
            {
                //Console.WriteLine("[{0}] {1}", token, e.Error.ToString());    
            }
            else
            {
                //Console.WriteLine("Mail Sent Successfully");    
            }
        }
    }
}
