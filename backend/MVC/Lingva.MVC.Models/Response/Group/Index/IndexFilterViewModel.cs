using Lingva.MVC.Models.Request;
using Lingva.MVC.Models.Response.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response.Group.Index
{
    [ExcludeFromCodeCoverage]
    public class IndexFilterViewModel
    {
        public IndexFilterViewModel(List<LanguageViewModel> languages, Dictionary<string, FilterModel> filters)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            languages.Insert(0, new LanguageViewModel { Name = "Все", Id = 0 });

            filters.TryGetValue("Language", out FilterModel filterLanguage);
            filters.TryGetValue("Name", out FilterModel filterName);

            int selectedLanguage = 0;
            if (filterLanguage != null && Int32.TryParse(filterLanguage.PropertyValue.ToString(), out selectedLanguage))
            {
                SelectedLanguage = selectedLanguage;
            }
            Languages = new SelectList(languages, "Id", "Name", selectedLanguage);

            string selectedName = String.Empty;
            if (filterName != null)
            {
                SelectedName = filterName.PropertyValue.ToString();
            }
        }
        public SelectList Languages { get; private set; }
        public int? SelectedLanguage { get; private set; }
        public string SelectedName { get; private set; }
    }
}
