using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace TestModuleA;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TestModuleADomainSharedModule)
)]
public class TestModuleADomainModule : AbpModule
{

}
