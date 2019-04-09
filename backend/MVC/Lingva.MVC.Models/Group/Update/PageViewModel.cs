using Lingva.MVC.Models.Request;
using Lingva.MVC.Models.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lingva.MVC.Models.Group.Update
{
    public class PageViewModel
    {
        public GroupCreateViewModel GroupViewModel { get; set; }
        public SelectList Languages { get; private set; }

        public PageViewModel(GroupCreateViewModel groupViewModel, IList<LanguageViewModel> languages)
        {
            GroupViewModel = groupViewModel;
            Languages = new SelectList(languages, "Id", "Name", groupViewModel.LanguageId);
        }
    }
}
