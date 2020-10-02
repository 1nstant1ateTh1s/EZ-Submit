using EZSubmitApp.Core.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZSubmitApp.Infrastructure.Paging
{
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Private constructor called by the CreateAsync method.
        /// </summary>
        private PagedList(IEnumerable<T> data, int count, int pageIndex, int pageSize)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        #region Methods
        /// <summary>
        /// Pages an IQueryable source.
        /// </summary>
        /// <param name="query">An IQueryable source of generic type</param>
        /// <param name="pagingArgs">Specifies paging values</param>
        /// <returns>
        /// An object containing the paged result
        /// and all the relevant paging navigation info.
        /// </returns>
        public static async Task<PagedList<T>> CreateAsync(
            IQueryable<T> query, 
            PageSearchArgs pagingArgs)
        {
            var count = await query.CountAsync();
            query = query
                .Skip(pagingArgs.PageIndex * pagingArgs.PageSize)
                .Take(pagingArgs.PageSize);

            var data = await query.ToListAsync();

            return new PagedList<T>(
                data, 
                count, 
                pagingArgs.PageIndex, 
                pagingArgs.PageSize);
        }
        #endregion

        #region Properties
        /// <summary>
        /// The data result.
        /// </summary>
        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// Zero-based index of current page.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Total items count
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Total pages count
        /// </summary>
        public int TotalPages { get; private set; }

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
