using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Features;

namespace TestModuleA.Samples;

public class SampleAppService : TestModuleAAppService, ISampleAppService
{
    private readonly IFeatureChecker _featureChecker;

    public SampleAppService(IFeatureChecker featureChecker)
    {
        _featureChecker = featureChecker;
    }

    public async Task<SampleDto> GetAsync()
    {
        bool featureValue = await _featureChecker.GetAsync<bool>("TestModuleA.Feature1");

        return (
            new SampleDto
            {
                FeatureName = "TestModuleA.Feature1",
                FeatureValue = featureValue
            });
    }

    //[Authorize]
    [RequiresFeature("TestModuleA.Feature1")]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return (
            new SampleDto
            {
                FeatureName = "TestModuleA.Feature1",
                FeatureValue = await _featureChecker.GetAsync<bool>("TestModuleA.Feature1")
            });
    }
}
