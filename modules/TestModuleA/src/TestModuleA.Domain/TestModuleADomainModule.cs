using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace TestModuleA;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TestModuleADomainSharedModule)
)]
[DependsOn(typeof(AbpSettingManagementDomainModule))]
public class TestModuleADomainModule : AbpModule
{

}
