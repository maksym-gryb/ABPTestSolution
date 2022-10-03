using Volo.Abp.Reflection;

namespace TestModuleA.Permissions;

public class TestModuleAPermissions
{
    public const string GroupName = "TestModuleA";

    public const string SettingManagement = $"{GroupName}.SettingManagement";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TestModuleAPermissions));
    }
}
