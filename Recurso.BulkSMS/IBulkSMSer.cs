using System.Threading.Tasks;

namespace Recurso.BulkSMS
{
    public interface IBulkSMSer
    {
        int LongMessageMaximumParts { get; set; }
        string Password { get; set; }
        string Username { get; set; }

        Task<SMSProfile> GetProfile();
        Task<SMSResponse> SendSMS(string phoneNumber, string message);
    }
}