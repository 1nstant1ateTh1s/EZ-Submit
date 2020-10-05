using System.Collections.Generic;

namespace EZSubmitApp.Core.Paging
{
    public abstract class BasePagedList<T> : IPagedList<T>
    {
        #region Properties
        /// <summary>
        /// The data result.
        /// </summary>
        public IEnumerable<T> Data { get; protected set; }

        /// <summary>
        /// Zero-based index of current page.
        /// </summary>
        public int PageIndex { get; protected set; }

        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; protected set; }

        /// <summary>
        /// Total items count
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// Total pages count
        /// </summary>
        public int TotalPages { get; protected set; }

        /// <summary>
        /// TRUE if the current page has a previous page,
        /// FALSE otherwise.
        /// </summary>
        public bool HasPreviousPage => PageIndex > 0;

        /// <summary>
        /// TRUE if the current page has a next page,
        /// FALSE otherwise.
        /// </summary>
        public bool HasNextPage => PageIndex + 1 < TotalPages;
        #endregion
    }
}
