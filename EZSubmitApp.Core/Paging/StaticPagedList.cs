using System;
using System.Collections.Generic;

namespace EZSubmitApp.Core.Paging
{
    public class StaticPagedList<T> : BasePagedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the PagedList class that contains the already paged data and the 
        /// associated paging metadata.
        /// </summary>
        /// <param name="data">The already divided subset of data.</param>
        /// <param name="count">Total items count contained within this instance.</param>
        /// <param name="pageIndex">Zero-based index of current page contained within this instance.</param>
        /// <param name="pageSize">Number of items contained in each page within this instance.</param>
        public StaticPagedList(IEnumerable<T> data, int count, int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }
    }
}
