using TestModuleA.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TestModuleA.Permissions;

public class TestModuleAPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestModuleAPermissions.GroupName, L("Permission:TestModuleA"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestModuleAResource>(name);
    }
}
