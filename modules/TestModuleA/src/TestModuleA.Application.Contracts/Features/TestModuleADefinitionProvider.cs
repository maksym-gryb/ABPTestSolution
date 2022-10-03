using TestModuleA.Localization;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace TestModuleA.Features;

public class TestModuleADefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var myGroup = context.AddGroup("TestModuleA");

        myGroup.AddFeature("TestModuleA.Feature1",
            defaultValue: "false",
            displayName: LocalizableString.Create<TestModuleAResource>("Feature1"),
            valueType: new ToggleStringValueType());

        myGroup.AddFeature("TestModuleA.Feature2",
            defaultValue: "10",
            displayName: LocalizableString.Create<TestModuleAResource>("Feature2"),
            valueType: new FreeTextStringValueType(new NumericValueValidator(0, 1000000)));
    }
}