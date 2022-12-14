using TestModuleA.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TestModuleA;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(TestModuleAEntityFrameworkCoreTestModule)
    )]
public class TestModuleADomainTestModule : AbpModule
{

}
