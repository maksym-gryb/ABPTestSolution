using Volo.Abp.Reflection;

namespace TestModuleA.Permissions;

public class TestModuleAPermissions
{
    public const string GroupName = "TestModuleA";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TestModuleAPermissions));
    }
}
