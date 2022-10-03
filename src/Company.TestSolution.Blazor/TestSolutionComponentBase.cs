using Company.TestSolution.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Company.TestSolution.Blazor;

public abstract class TestSolutionComponentBase : AbpComponentBase
{
    protected TestSolutionComponentBase()
    {
        LocalizationResource = typeof(TestSolutionResource);
    }
}
