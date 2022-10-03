using Volo.Abp.Settings;

namespace Company.TestSolution.Settings;

public class TestSolutionSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TestSolutionSettings.MySetting1));
    }
}
