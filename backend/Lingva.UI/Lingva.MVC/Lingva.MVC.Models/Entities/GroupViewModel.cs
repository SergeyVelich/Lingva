using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class GroupViewModel
    {
        private DateTime? date = null;

        public int Id { get; set; }
        [Display(Name = "Title")]
        [BindRequired]
        public string Name { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date
        {
            get
            {
                return date.HasValue
                   ? this.date.Value
                   : DateTime.Now;
            }

            set { date = value; }
        }

        [Display(Name = "Language ID")]
        public int LanguageId { get; set; }
        [Display(Name = "Language")]
        public string LanguageName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
