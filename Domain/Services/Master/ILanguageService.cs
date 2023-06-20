using ComplyExchangeCMS.Domain.Entities.Masters;
using ComplyExchangeCMS.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services.Master
{
    public interface ILanguageService
    {
        Task<IReadOnlyList<LanguageView>> GetAllAsync();
        Task<int> InsertLanguage(LanguageInsert languageModel);
        Task<int> UpdateLanguage(LanguageUpdate languageModel);
        Task<int> DeleteLanguage(int Id);
        Task<LanguageView> GetByIdAsync(int Id);
    }
}
