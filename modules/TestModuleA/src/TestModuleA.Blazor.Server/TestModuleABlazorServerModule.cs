using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace TestModuleA.Blazor.Server;

[DependsOn(
    typeof(TestModuleABlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class TestModuleABlazorServerModule : AbpModule
{

}
