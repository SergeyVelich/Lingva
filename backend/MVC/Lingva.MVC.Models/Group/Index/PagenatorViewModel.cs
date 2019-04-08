using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Group.Index
{
    [ExcludeFromCodeCoverage]
    public class PagenatorViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PagenatorViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
