using Company.TestSolution.Localization;
using Volo.Abp.Application.Services;

namespace Company.TestSolution;

/* Inherit your application services from this class.
 */
public abstract class TestSolutionAppService : ApplicationService
{
    protected TestSolutionAppService()
    {
        LocalizationResource = typeof(TestSolutionResource);
    }
}
