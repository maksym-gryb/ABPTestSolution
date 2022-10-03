using Company.TestSolution.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Company.TestSolution;

[DependsOn(
    typeof(TestSolutionEntityFrameworkCoreTestModule)
    )]
public class TestSolutionDomainTestModule : AbpModule
{

}
