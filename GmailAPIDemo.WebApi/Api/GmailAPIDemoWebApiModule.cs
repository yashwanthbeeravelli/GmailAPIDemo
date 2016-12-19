using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace GmailAPIDemo.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(GmailAPIDemoApplicationModule))]
    public class GmailAPIDemoWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(GmailAPIDemoApplicationModule).Assembly, "app")
                .Build();
            //DynamicApiControllerBuilder
            //    .ForAll<IApplicationService>(Assembly.GetAssembly(typeof(GmailAPIDemoApplicationModule)), "tasksystem")
            //    .Build();
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
