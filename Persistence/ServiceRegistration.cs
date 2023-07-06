using ComplyExchangeCMS.Domain.Services;
using ComplyExchangeCMS.Domain.Services.Master;
using ComplyExchangeCMS.Persistence.Services;
using ComplyExchangeCMS.Persistence.Services.Master;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection; 

namespace ComplyExchangeCMS.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IContentManagementService, ContentManagementService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IFormTypesService, FormTypesService>();
            services.AddScoped<IDocumentationService, DocumentationService>();
            services.AddScoped<ILOBService, LOBService>();
            services.AddScoped<ICapacitiesService, CapacitiesService>();
            services.AddScoped<IFormInstructionsService, FormInstructionsService>();
            services.AddScoped<IAgentEditListService, AgentEditListService>();
            services.AddScoped<IRuleService, RuleService>();
            services.AddScoped<IEasyHelpService, EasyHelpService>();
            services.AddScoped<ISettingService,SettingService>();

            services.AddScoped<IAgentFormTypeService, AgentFormTypeService>();
            services.AddScoped<IAgentUSSourceIncomeService, AgentUSSourceIncomeService>();
        }
    }
}
