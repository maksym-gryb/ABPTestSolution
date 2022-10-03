using TestModuleA.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TestModuleA;

public abstract class TestModuleAController : AbpControllerBase
{
    protected TestModuleAController()
    {
        LocalizationResource = typeof(TestModuleAResource);
    }
}
