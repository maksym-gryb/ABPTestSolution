using System;
using System.IO;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Company.TestSolution.Blazor.Menus;
using Company.TestSolution.Localization;
using Company.TestSolution.MultiTenancy;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components.Server.LeptonTheme;
using Volo.Abp.AspNetCore.Components.Server.LeptonTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.Blazor.Server;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Pro.Blazor.Server;
using Volo.Abp.IdentityServer.Blazor.Server;
using Volo.Abp.LanguageManagement.Blazor.Server;
using Volo.Abp.LeptonTheme.Management.Blazor.Server;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TextTemplateManagement.Blazor.Server;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas.Host.Blazor.Server;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using Company.TestSolution.Blazor.Components.Layout;
using StackExchange.Redis;
using Volo.Abp.Account.Pro.Admin.Blazor.Server;
using Volo.Abp.Account.Pro.Public.Blazor.Server;
using Volo.Abp.Account.Public.Web.Impersonation;
using Volo.Abp.Identity.Pro.Blazor;
using Volo.Saas.Host.Blazor;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Gdpr.Blazor.Server;
using Volo.Abp.Http.Client.Web;
using Volo.Abp.Http.Client.IdentityModel.Web;
using TestModuleA.Blazor.Server;

namespace Company.TestSolution.Blazor;

[DependsOn(
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAspNetCoreMvcClientModule),
    typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
    typeof(AbpHttpClientWebModule),
    typeof(AbpAccountPublicWebImpersonationModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpAspNetCoreMvcUiLeptonThemeModule),
    typeof(AbpAspNetCoreComponentsServerLeptonThemeModule),
    typeof(AbpAccountAdminBlazorServerModule),
    typeof(AbpAccountPublicBlazorServerModule),
    typeof(AbpAuditLoggingBlazorServerModule),
    typeof(AbpIdentityProBlazorServerModule),
    typeof(LeptonThemeManagementBlazorServerModule),
    typeof(AbpIdentityServerBlazorServerModule),
    typeof(LanguageManagementBlazorServerModule),
    typeof(SaasHostBlazorServerModule),
    typeof(TextTemplateManagementBlazorServerModule),
    typeof(AbpGdprBlazorServerModule),
    typeof(TestSolutionHttpApiClientModule)
    )]
[DependsOn(typeof(TestModuleABlazorServerModule))]
    public class TestSolutionBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(TestSolutionResource),
                typeof(TestSolutionDomainSharedModule).Assembly,
                typeof(TestSolutionApplicationContractsModule).Assembly,
                typeof(TestSolutionBlazorModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAuthentication(context, configuration);
        ConfigureImpersonation(context, configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureLocalizationServices();
        ConfigureSwaggerServices(context.Services);
        ConfigureCache(configuration);
        ConfigureDataProtection(context, configuration, hostingEnvironment);
        ConfigureBlazorise(context);
        ConfigureRouter();
        ConfigureMenu(configuration);
        ConfigureLeptonTheme();
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            // MVC UI
            options.StyleBundles.Configure(
                LeptonThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );

            // Blazor UI
            options.StyleBundles.Configure(
                BlazorLeptonThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/blazor-global-styles.css");
                    //You can remove the following line if you don't use Blazor CSS isolation for components
                    bundle.AddFiles("/Company.TestSolution.Blazor.styles.css");
                }
            );
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
            })
            .AddAbpOpenIdConnect("oidc", options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);;
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                options.ClientId = configuration["AuthServer:ClientId"];
                options.ClientSecret = configuration["AuthServer:ClientSecret"];

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("role");
                options.Scope.Add("email");
                options.Scope.Add("phone");
                options.Scope.Add("TestSolution");
            });
    }

    private void ConfigureImpersonation(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.Configure<SaasHostBlazorOptions>(options =>
        {
            options.EnableTenantImpersonation = true;
        });
        context.Services.Configure<AbpIdentityProBlazorOptions>(options =>
        {
            options.EnableUserImpersonation = true;
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<TestSolutionDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Company.TestSolution.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<TestSolutionApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Company.TestSolution.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<TestSolutionBlazorModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TestSolution API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureLocalizationServices()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "??????????????", "ae"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "??e??tina"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Fran??ais", "fr"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Portugu??s"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "??????????????", "ru"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", "sk"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "T??rk??e"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "????????????"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "????????????"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Espa??ol"));
        });
    }

    private void ConfigureCache(IConfiguration configuration)
    {
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "TestSolution:";
        });
    }

    private void ConfigureDataProtection(
        ServiceConfigurationContext context,
        IConfiguration configuration,
        IWebHostEnvironment hostingEnvironment)
    {
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("TestSolution");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "TestSolution-Protection-Keys");
        }
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private void ConfigureMenu(IConfiguration configuration)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TestSolutionMenuContributor(configuration));
        });
    }

    private void ConfigureLeptonTheme()
    {
        Configure<Volo.Abp.AspNetCore.Components.Web.LeptonTheme.LeptonThemeOptions>(options =>
        {
            options.FooterComponent = typeof(MainFooterComponent);
        });
    }

    private void ConfigureRouter()
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(TestSolutionBlazorModule).Assembly;
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TestSolutionBlazorModule>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseAbpSecurityHeaders();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "TestSolution API");
        });
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
