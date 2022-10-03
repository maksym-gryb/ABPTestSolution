using TestModuleA.Localization;
using Volo.Abp.Application.Services;

namespace TestModuleA;

public abstract class TestModuleAAppService : ApplicationService
{
    protected TestModuleAAppService()
    {
        LocalizationResource = typeof(TestModuleAResource);
        ObjectMapperContext = typeof(TestModuleAApplicationModule);
    }
}
