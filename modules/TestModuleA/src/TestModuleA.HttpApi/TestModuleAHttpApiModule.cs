using Localization.Resources.AbpUi;
using TestModuleA.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace TestModuleA;

[DependsOn(
    typeof(TestModuleAApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class TestModuleAHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TestModuleAHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TestModuleAResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
