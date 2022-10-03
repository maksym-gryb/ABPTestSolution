using System.Threading.Tasks;
using TestModuleA.Localization;
using Volo.Abp.UI.Navigation;

namespace TestModuleA.Blazor.Menus;

public class TestModuleAMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<TestModuleAResource>();

        context.Menu.AddItem(new ApplicationMenuItem(TestModuleAMenus.Prefix, displayName: "Sample Page", "/TestModuleA", icon: "fa fa-globe"));

        await Task.CompletedTask;
    }
}
