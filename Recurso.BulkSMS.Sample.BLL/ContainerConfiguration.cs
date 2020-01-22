using Autofac;
using Recurso.BulkSMS.Common.Interfaces;
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

            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<RestRequest>().As<IRestRequest>();

            builder.RegisterType<BulkSMSProfile>().PropertiesAutowired().As<IProfile>();
            builder.RegisterType<AccountProfile>().As<IAccountProfile>();

            builder.RegisterType<BulkSMSTextMessage>().PropertiesAutowired().As<ITextMessage>();
            builder.RegisterType<SendMessage>().As<ISendMessage>();
            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();

            return builder.Build();
        }
    }
}
