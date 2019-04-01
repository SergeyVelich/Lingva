using Lingva.MVC.ViewModel.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lingva.MVC.Models.Response
{
    public class FilterViewModel
    {
        public FilterViewModel(List<LanguageViewModel> languages, int? language, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            languages.Insert(0, new LanguageViewModel { Name = "Все", Id = 0 });
            Languages = new SelectList(languages, "Id", "Name", language);
            SelectedLanguage = language;
            SelectedName = name;
        }
        public SelectList Languages { get; private set; } // список компаний
        public int? SelectedLanguage { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}
