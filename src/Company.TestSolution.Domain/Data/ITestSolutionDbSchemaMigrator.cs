using System.Threading.Tasks;

namespace Company.TestSolution.Data;

public interface ITestSolutionDbSchemaMigrator
{
    Task MigrateAsync();
}
