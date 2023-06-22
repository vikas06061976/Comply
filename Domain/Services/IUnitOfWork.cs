using ComplyExchangeCMS.Domain.Services;
using ComplyExchangeCMS.Domain.Services.Master;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUnitOfWork
    {
        IProductService Products { get; }
        IPageService Pages { get; }
        IContentManagementService ContentManagement { get; }
        IAgentService Agents { get; }
        ICountryService Countries { get; }
        ILanguageService Languages { get; }
        IFormTypesService FormTypes { get; }
        IDocumentationService Documentation { get; }
        ILOBService LOBService { get; }
        ICapacitiesService CapacitiesService { get; }
        IFormInstructionsService FormInstructionsService { get; }

    }
}
