using Lingva.MVC.Models.Request;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response.Group.Index
{
    [ExcludeFromCodeCoverage]
    public class IndexSortViewModel
    {
        public string SortProperty { get; set; }
        public string SortOrder { get; set; }
        public string CurrentSortProperty { get; set; }

        //public SortState NameSort { get; set; }
        //public SortState DateSort { get; set; }
        //public SortState LanguageSort { get; set; }
        //public SortState Current { get; set; }
        //public bool Up { get; set; }

        public IndexSortViewModel(string sortProperty, string sortOrder)
        {
            SortProperty = sortProperty;
            SortOrder = sortOrder;
            //// значения по умолчанию
            //NameSort = SortState.NameAsc;
            //DateSort = SortState.DateAsc;
            //LanguageSort = SortState.LanguageAsc;
            //Up = true;

            //if (sortOrder == SortState.DateDesc || sortOrder == SortState.NameDesc
            //    || sortOrder == SortState.LanguageDesc)
            //{
            //    Up = false;
            //}

            //switch (sortOrder)
            //{
            //    case SortState.NameDesc:
            //        Current = NameSort = SortState.NameAsc;
            //        break;
            //    case SortState.DateAsc:
            //        Current = DateSort = SortState.DateDesc;
            //        break;
            //    case SortState.DateDesc:
            //        Current = DateSort = SortState.DateAsc;
            //        break;
            //    case SortState.LanguageAsc:
            //        Current = LanguageSort = SortState.LanguageDesc;
            //        break;
            //    case SortState.LanguageDesc:
            //        Current = LanguageSort = SortState.LanguageAsc;
            //        break;
            //    default:
            //        Current = NameSort = SortState.NameDesc;
            //        break;
            //}
        }
    }
}
