using System.Threading.Tasks;

namespace Smeat.Leader.Infrastructure.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}