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

        public virtual int Page { get; set; }
        public virtual int PageRecords { get; set; }
        public virtual int TotalRecords { get; set; }

        public GroupsListOptionsModel()
        {
            SortProperty = "Name";
            SortOrder = "Desc";

            //Name = "";
            //LanguageId = 0;
            //DateFrom = DateTime.MinValue;
            //DateTo = DateTime.MaxValue;

            Page = 1;
            PageRecords = 5;
            TotalRecords = 15;
        }
    }
}
