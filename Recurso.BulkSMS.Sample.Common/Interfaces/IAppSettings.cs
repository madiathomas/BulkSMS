using System;
using System.Collections.Generic;
using System.Text;

namespace Recurso.BulkSMS.Sample.Common.Interfaces
{
    public interface IAppSettings
    {
        string GetSetting(string settingName);
    }
}
