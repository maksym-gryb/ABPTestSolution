using Microsoft.Extensions.DependencyInjection;
using TestModuleA.Blazor.Menus;
using TestModuleA.Blazor.Settings;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.Settings;
using Volo.Abp.UI.Navigation;

namespace TestModuleA.Blazor;

[DependsOn(
    typeof(TestModuleAApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(typeof(AbpSettingManagementDomainModule))]
public class TestModuleABlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TestModuleABlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<TestModuleABlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TestModuleAMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(TestModuleABlazorModule).Assembly);
        });

        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new SettingComponentContributor());
        });
    }
}
