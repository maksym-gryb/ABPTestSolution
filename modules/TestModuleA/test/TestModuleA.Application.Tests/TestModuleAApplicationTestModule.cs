using Volo.Abp.Modularity;

namespace TestModuleA;

[DependsOn(
    typeof(TestModuleAApplicationModule),
    typeof(TestModuleADomainTestModule)
    )]
public class TestModuleAApplicationTestModule : AbpModule
{

}
