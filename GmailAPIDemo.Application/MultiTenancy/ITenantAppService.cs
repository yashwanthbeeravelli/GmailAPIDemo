using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GmailAPIDemo.MultiTenancy.Dto;

namespace GmailAPIDemo.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
