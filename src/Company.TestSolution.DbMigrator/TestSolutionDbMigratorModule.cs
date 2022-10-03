using Company.TestSolution.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Company.TestSolution.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TestSolutionEntityFrameworkCoreModule),
    typeof(TestSolutionApplicationContractsModule)
)]
public class TestSolutionDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
    }
}
