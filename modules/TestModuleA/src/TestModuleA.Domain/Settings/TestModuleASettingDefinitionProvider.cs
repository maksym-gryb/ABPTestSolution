using TestModuleA.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace TestModuleA.Settings;

public class TestModuleASettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(TestModuleASettings.Setting1Name,
            "Setting1Name test setting data",
            L("DisplayName:TestModuleASettings.Setting1Name"),
            L("Description:TestModuleASettings.Setting1Name"),
            isVisibleToClients: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestModuleAResource>(name);
    }
}
