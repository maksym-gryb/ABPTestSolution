using Volo.Abp.Modularity;

namespace Company.TestSolution;

[DependsOn(
    typeof(TestSolutionApplicationModule),
    typeof(TestSolutionDomainTestModule)
    )]
public class TestSolutionApplicationTestModule : AbpModule
{

}
