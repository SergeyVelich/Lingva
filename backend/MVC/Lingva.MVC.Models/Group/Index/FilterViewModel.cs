using Lingva.MVC.Models.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Group.Index
{
    [ExcludeFromCodeCoverage]
    public class FilterViewModel
    {
        public FilterViewModel(IList<LanguageViewModel> languages,  string name, int? language, string description, DateTime date)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            languages.Insert(0, new LanguageViewModel { Name = "Все", Id = 0 });           
            Languages = new SelectList(languages, "Id", "Name", language);
            SelectedLanguage = language;
            SelectedName = name;
        }
        public SelectList Languages { get; private set; }
        public int? SelectedLanguage { get; private set; }
        public string SelectedName { get; private set; }
    }
}
