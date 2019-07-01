using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class GroupViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        [BindRequired]
        public string Name { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Display(Name = "Language ID")]
        public int LanguageId { get; set; }
        [Display(Name = "Language")]
        public string LanguageName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
