using TestModuleA.Localization;
using Volo.Abp.AspNetCore.Components;

namespace TestModuleA.Blazor;

public abstract class TestModuleAComponentBase : AbpComponentBase
{
    protected TestModuleAComponentBase()
    {
        LocalizationResource = typeof(TestModuleAResource);
    }
}
