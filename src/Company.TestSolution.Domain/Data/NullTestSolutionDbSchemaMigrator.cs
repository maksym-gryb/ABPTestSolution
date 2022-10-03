using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Company.TestSolution.Data;

/* This is used if database provider does't define
 * ITestSolutionDbSchemaMigrator implementation.
 */
public class NullTestSolutionDbSchemaMigrator : ITestSolutionDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
