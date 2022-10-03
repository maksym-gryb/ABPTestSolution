using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace TestModuleA;

[DependsOn(
    typeof(TestModuleADomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class TestModuleAApplicationContractsModule : AbpModule
{

}
