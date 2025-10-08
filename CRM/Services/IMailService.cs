using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IMailService
    {
        Task SendMail(string userEmail, string subject, string body);
    }
}