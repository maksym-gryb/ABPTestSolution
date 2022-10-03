using Company.TestSolution.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Company.TestSolution.Permissions;

public class TestSolutionPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestSolutionPermissions.GroupName);

        myGroup.AddPermission(TestSolutionPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(TestSolutionPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(TestSolutionPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestSolutionResource>(name);
    }
}
