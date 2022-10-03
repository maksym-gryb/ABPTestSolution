using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Company.TestSolution;

[Dependency(ReplaceServices = true)]
public class TestSolutionBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TestSolution";
}
