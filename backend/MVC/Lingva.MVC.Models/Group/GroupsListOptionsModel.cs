using Lingva.MVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.MVC.Models.Group.Index
{
    public class GroupsListOptionsModel
    {
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Display(Name = "Language")]
        public int? LanguageId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string SortProperty { get; set; }
        public string SortOrder { get; set; }

        public int Page { get; set; }
        public int PageRecords { get; set; }
        public int TotalRecords { get; set; }

        public IEnumerable<GroupViewModel> Groups { get; set; }

        public GroupsListOptionsModel()
        {
            SortProperty = "Name";
            SortOrder = "Desc";

            Name = "";
            LanguageId = 0;

            Page = 1;
            PageRecords = 5;
            TotalRecords = 15;
        }
    }
}
