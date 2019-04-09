using Lingva.MVC.Models.Request;
using Lingva.MVC.Models.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lingva.MVC.Models.Group.Create
{
    public class PageViewModel
    {
        public GroupCreateViewModel GroupViewModel { get; set; }
        public SelectList Languages { get; private set; }

        public PageViewModel(IList<LanguageViewModel> languages)
        {
            Languages = new SelectList(languages, "Id", "Name", 1);
        }
    }
}
