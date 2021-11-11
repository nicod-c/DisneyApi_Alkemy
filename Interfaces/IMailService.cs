using AlkemyDisney.Models;
using System.Threading.Tasks;

namespace AlkemyDisney.Services
{
    public interface IMailService
    {
        Task SendEmail(User user);
    }
}