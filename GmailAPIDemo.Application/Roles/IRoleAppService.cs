using System.Threading.Tasks;
using Abp.Application.Services;
using GmailAPIDemo.Roles.Dto;

namespace GmailAPIDemo.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
