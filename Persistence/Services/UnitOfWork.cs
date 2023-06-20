﻿using ComplyExchangeCMS.Domain.Services;
using ComplyExchangeCMS.Domain.Services.Master;
using Domain.Services;
using System.Data;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductService Products { get; }
        public IPageService Pages { get; }
        public IContentManagementService ContentManagement { get; }
        public IAgentService Agents { get; }
        public ICountryService Countries { get; }
        public ILanguageService Languages { get; }
        public IFormTypesService FormTypes { get; }
        public IDocumentationService Documentation { get; }
        public ILOBService LOBService { get; }
        public UnitOfWork(IProductService productService, IPageService pageService, 
            IContentManagementService contentManagementService, IAgentService agentService, 
            ICountryService countryService, ILanguageService languageService, 
            IFormTypesService formTypesService, IDocumentationService documentationService, ILOBService lOBService)
        {
            Products = productService;
            Pages = pageService;
            ContentManagement = contentManagementService;
            Agents = agentService;
            Countries = countryService;
            Languages = languageService;
            FormTypes = formTypesService;
            Documentation = documentationService;
            LOBService = lOBService;
        }       
    }
}