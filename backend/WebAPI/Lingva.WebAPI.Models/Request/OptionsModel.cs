using Lingva.WebAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class OptionsModel
    {
        [Display(Name = "Title")]
        public virtual string Name { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public virtual DateTime Date { get; set; }
        [Display(Name = "Language")]
        public virtual int? LanguageId { get; set; }
        [Display(Name = "Description")]
        public virtual string Description { get; set; }
        public virtual string SortProperty { get; set; }
        public virtual string SortOrder { get; set; }

        public virtual int Page { get; set; }
        public virtual int PageRecords { get; set; }
        public virtual int TotalRecords { get; set; }

        public virtual IEnumerable<GroupViewModel> Groups { get; set; }

        public OptionsModel()
        {
            SortProperty = "Name";
            SortOrder = "Desc";

            Page = 1;
            PageRecords = 5;
        }
    }
}
