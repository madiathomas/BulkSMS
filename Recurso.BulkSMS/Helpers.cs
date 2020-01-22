using System;

namespace Recurso.BulkSMS
{
    public static class Helpers
    {
        internal static void CheckIfFieldIsMissing(this string field)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                throw new MissingFieldException($"{field} is required");
            }
        }
    }
}
