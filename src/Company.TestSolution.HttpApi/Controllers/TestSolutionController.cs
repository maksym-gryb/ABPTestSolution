using Company.TestSolution.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Company.TestSolution.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TestSolutionController : AbpControllerBase
{
    protected TestSolutionController()
    {
        LocalizationResource = typeof(TestSolutionResource);
    }
}
