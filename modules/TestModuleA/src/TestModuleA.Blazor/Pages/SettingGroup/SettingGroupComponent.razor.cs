using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;

namespace TestModuleA.Blazor.Pages.SettingGroup;

public partial class SettingGroupComponent
{
    [Inject]
    protected ISettingManager SettingManager { get; set; }
    //[Inject]
    //protected ISettingStore SettingStore { get; set; }

    protected List<SettingValue> Settings { get; set; } = new();

    private SnackbarStack snackbar;

    protected override async Task OnInitializedAsync()
    {
        var allSettings = await SettingManager.GetAllGlobalAsync();

        Settings = allSettings.Where(s => s.Name.StartsWith("TestModuleA")).ToList();

        await base.OnInitializedAsync();
    }

    protected async Task SaveSettings()
    {
        foreach (var setting in Settings)
            await SettingManager.SetGlobalAsync(setting.Name, setting.Value);

        await snackbar.PushAsync(L["Settings:Save:Success"], SnackbarColor.Success);
    }
}
