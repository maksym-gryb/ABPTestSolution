using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Company.TestSolution.Blazor;

[Dependency(ReplaceServices = true)]
public class TestSolutionBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TestSolution";
}
