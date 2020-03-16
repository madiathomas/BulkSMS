using Autofac;

using Recurso.BulkSMS.Sample.Common.Interfaces;
using Recurso.BulkSMS.Sample.DAL;
using RestSharp;

namespace Recurso.BulkSMS.Sample.BLL
{
    public class ContainerConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppSettings>().As<IAppSettings>();
            var test  = builder.RegisterType<BulkSMSProfile>().PropertiesAutowired().As<IProfile>();

            builder.RegisterType<AccountProfile>().As<IAccountProfile>();
            
            builder.RegisterType<BulkSMSTextMessage>().PropertiesAutowired().As<ITextMessage>();
            
            builder.RegisterType<SendMessage>().As<ISendMessage>();
            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();
            builder.RegisterType<Application>().As<IApplication>();

            return builder.Build();
        }
    }
}
