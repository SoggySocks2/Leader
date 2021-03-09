using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Smeat.Leader.Web.Pages.Shared
{
    public class _PaginationModel : PageModel
    {
        public int TotalItems { get; private set; }
        public int ItemsPerPage { get; private set; }
        public int ActualPage { get; private set; }
        public int TotalPages { get; private set; }
        public string Previous { get; private set; }
        public string Next { get; private set; }

        public _PaginationModel (int count, int skip, int take)
        {
            if (count < 0) throw new ArgumentException(string.Format("Count {0} is less than zero", count));
            if (skip < 0) throw new ArgumentException(string.Format("Skip {0} is less than zero", skip));
            if (take < 1) throw new ArgumentException(string.Format("Take {0} is less than 1", take));

            TotalItems = count;
            ItemsPerPage = take;

            if(skip < TotalItems)
            {
                ActualPage = 1;
            }
            else
            {
                ActualPage = TotalItems / ItemsPerPage;
                if((TotalItems % ItemsPerPage) > 0) ActualPage++;
            }

            if (TotalItems < 1)
            {
                TotalPages = 0;
            }
            else if (TotalItems <= ItemsPerPage)
            {
                TotalPages = 1;
            }
            else
            {
                TotalPages = TotalItems / ItemsPerPage;
                if ((TotalItems % ItemsPerPage) > 0) TotalPages++;
            }

            Previous = (ActualPage - 1).ToString();
            Next = (ActualPage + 1).ToString();
        }
    }
}
