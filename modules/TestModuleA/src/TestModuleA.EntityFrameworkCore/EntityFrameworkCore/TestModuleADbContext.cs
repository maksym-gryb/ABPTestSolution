using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace TestModuleA.EntityFrameworkCore;

[ConnectionStringName(TestModuleADbProperties.ConnectionStringName)]
public class TestModuleADbContext : AbpDbContext<TestModuleADbContext>, ITestModuleADbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public TestModuleADbContext(DbContextOptions<TestModuleADbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTestModuleA();
    }
}
