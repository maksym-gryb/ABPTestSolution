using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Company.TestSolution.Data;
using Volo.Abp.DependencyInjection;

namespace Company.TestSolution.EntityFrameworkCore;

public class EntityFrameworkCoreTestSolutionDbSchemaMigrator
    : ITestSolutionDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTestSolutionDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the TestSolutionDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TestSolutionDbContext>()
            .Database
            .MigrateAsync();
    }
}
