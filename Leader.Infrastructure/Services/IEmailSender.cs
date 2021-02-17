using System.Threading.Tasks;

namespace Smeat.Leader.Infrastructure.Services
{
    public interface IEmailSender
    {
        Task<Task> SendEmailAsync(string[] recipients, string subject, string body);
        Task<Task> SendEmailAsync(string[] recipients, string subject, string body, bool bodyIsHtml);
        Task<Task> SendEmailAsync(string[] recipients, string subject, string body, System.Net.Mail.MailPriority priority);
        Task<Task> SendEmailAsync(string[] recipients, string subject, string body, string[] attachments);
        Task<Task> SendEmailAsync(string[] recipients, string subject, string body, bool bodyIsHtml, System.Net.Mail.MailPriority priority, string[] attachments);
    }
}