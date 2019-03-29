﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.ViewModel.Response
{
    [ExcludeFromCodeCoverage]
    public class GroupViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public string Date { get; set; }
        [Display(Name = "Language")]
        public string LanguageName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
    }
}
