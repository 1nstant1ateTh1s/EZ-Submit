using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.Paging
{
    public class PagedList<T> : BasePagedList<T>
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
    }
}
