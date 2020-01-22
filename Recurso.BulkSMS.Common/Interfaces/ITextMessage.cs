using System.Threading.Tasks;

namespace Recurso.BulkSMS.Common.Interfaces
{
    public interface ITextMessage
    {
        int LongMessageMaximumParts { get; set; }
        string Password { get; set; }
        string Username { get; set; }

        Task<SMSResponse> SendSMS(string phoneNumber, string message);
    }
}