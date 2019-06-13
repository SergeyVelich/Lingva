using Lingva.MVC.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Lingva.MVC.Models.Group
{
    public class GroupPageViewModel
    {
        public GroupViewModel GroupViewModel { get; set; }
        public SelectList Languages { get; private set; }

        public GroupPageViewModel(GroupViewModel groupViewModel, IList<LanguageViewModel> languages = null)
        {
            GroupViewModel = groupViewModel;

            if (languages == null)
            {
                languages = new List<LanguageViewModel>();
            }
            Languages = new SelectList(languages, "Id", "Name", groupViewModel.LanguageId);
        }
    }
}
