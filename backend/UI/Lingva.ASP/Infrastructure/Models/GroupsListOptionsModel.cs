using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.ASP.Infrastructure.Models
{
    [ExcludeFromCodeCoverage]
    public class GroupsListOptionsModel
    {
        [Display(Name = "Title")]
        public virtual string Name { get; set; }
        [Display(Name = "Date from")]   
        public virtual DateTime? DateFrom { get; set; }
        [Display(Name = "Date to")]
        public virtual DateTime? DateTo { get; set; }
        [Display(Name = "Language")]
        public virtual int? LanguageId { get; set; }
        [Display(Name = "Description")]
        public virtual string Description { get; set; }
        public virtual string SortProperty { get; set; }
        public virtual string SortOrder { get; set; }

        public virtual int PageIndex { get; set; }
        public virtual int PageSize { get; set; }

        public GroupsListOptionsModel()
        {
            SortProperty = "Name";
            SortOrder = "Desc";

            PageIndex = 1;
            PageSize = 5;
        }
    }
}
