﻿using System.Threading.Tasks;

namespace Recurso.BulkSMS
{
    public interface ITextMessage
    {
        string Username { get; set; }

        string Password { get; set; }

        int LongMessageMaximumParts { get; set; }

        Task SendSMS(string phoneNumber, string message);
    }
}