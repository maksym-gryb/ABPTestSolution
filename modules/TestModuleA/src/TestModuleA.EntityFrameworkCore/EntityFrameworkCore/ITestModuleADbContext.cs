using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace TestModuleA.EntityFrameworkCore;

[ConnectionStringName(TestModuleADbProperties.ConnectionStringName)]
public interface ITestModuleADbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
