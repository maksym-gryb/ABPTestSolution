using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace TestModuleA;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TestModuleAHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class TestModuleAConsoleApiClientModule : AbpModule
{

}
