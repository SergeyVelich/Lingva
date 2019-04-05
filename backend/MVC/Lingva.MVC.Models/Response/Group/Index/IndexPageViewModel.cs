using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response.Group.Index
{
    [ExcludeFromCodeCoverage]
    public class IndexPageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public IndexPageViewModel(int count, int pageNumber, int pageSize)
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
