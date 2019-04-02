using Lingva.MVC.Models.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class IndexFilterViewModel
    {
        public IndexFilterViewModel(List<LanguageViewModel> languages, FilterViewModel filters)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            languages.Insert(0, new LanguageViewModel { Name = "Все", Id = 0 });
            Languages = new SelectList(languages, "Id", "Name", filters.Language);
            SelectedLanguage = filters.Language;
            SelectedName = filters.Name;
        }
        public SelectList Languages { get; private set; }
        public int? SelectedLanguage { get; private set; }
        public string SelectedName { get; private set; }
    }
}
