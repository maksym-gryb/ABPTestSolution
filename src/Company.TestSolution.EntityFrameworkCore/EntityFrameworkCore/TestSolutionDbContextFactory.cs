using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Company.TestSolution.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class TestSolutionDbContextFactory : IDesignTimeDbContextFactory<TestSolutionDbContext>
{
    public TestSolutionDbContext CreateDbContext(string[] args)
    {
        TestSolutionEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TestSolutionDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new TestSolutionDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Company.TestSolution.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
