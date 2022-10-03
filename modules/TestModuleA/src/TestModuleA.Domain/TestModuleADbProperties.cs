namespace TestModuleA;

public static class TestModuleADbProperties
{
    public static string DbTablePrefix { get; set; } = "TestModuleA";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "TestModuleA";
}
