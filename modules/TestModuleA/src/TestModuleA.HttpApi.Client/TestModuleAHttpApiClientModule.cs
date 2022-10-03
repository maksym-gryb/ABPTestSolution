using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace TestModuleA;

[DependsOn(
    typeof(TestModuleAApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TestModuleAHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TestModuleAApplicationContractsModule).Assembly,
            TestModuleARemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TestModuleAHttpApiClientModule>();
        });
    }
}
