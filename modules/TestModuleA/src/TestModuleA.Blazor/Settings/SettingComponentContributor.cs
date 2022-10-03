using System.Threading.Tasks;
using Volo.Abp.SettingManagement.Blazor;
using TestModuleA.Blazor.Pages.SettingGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TestModuleA.Permissions;

namespace TestModuleA.Blazor.Settings;

public class SettingComponentContributor : ISettingComponentContributor
{
    public virtual async Task ConfigureAsync(SettingComponentCreationContext context)
    {
        if (!await CheckPermissionsInternalAsync(context))
        {
            return;
        }

        context.Groups.Add(
            new SettingComponentGroup(
                "TestModuleA.SettingGroup",
                "TestModuleA SettingGroup",// TODO: localize this
                typeof(SettingGroupComponent)
            )
        );
    }

    public virtual async Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context)
    {
        return await CheckPermissionsInternalAsync(context);
    }

    protected virtual async Task<bool> CheckPermissionsInternalAsync(SettingComponentCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAsync(TestModuleAPermissions.SettingManagement);
    }
}