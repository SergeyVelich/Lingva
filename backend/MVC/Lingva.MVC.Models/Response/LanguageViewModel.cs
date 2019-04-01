﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.ViewModel.Response
{
    [ExcludeFromCodeCoverage]
    public class LanguageViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
