using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.Paging
{
    public class PagedList<T> : BasePagedList<T>
    {
        /// <summary>
        /// Private constructor called by the CreateAsync method.
        /// </summary>
        private PagedList(IEnumerable<T> data, int count, int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }

        #region Methods
        /// <summary>
        /// Pages and/or sorts an IQueryable source.
        /// </summary>
        /// <param name="query">An IQueryable source of generic type</param>
        /// <param name="pagingArgs">Specifies paging values</param>
        /// <returns>
        /// An object containing the IQueryable paged/sorted result
        /// and all the relevant paging/sorting navigation info.
        /// </returns>
        public static async Task<PagedList<T>> CreateAsync(
            IQueryable<T> query,
            PageSearchRequest pagingArgs)
            //PageSearchArgs pagingArgs)
        {
            var count = await query.CountAsync();

            if (!String.IsNullOrEmpty(pagingArgs.SortColumn) 
                && IsValidProperty(pagingArgs.SortColumn))
            {
                pagingArgs.SortOrder = !String.IsNullOrEmpty(pagingArgs.SortOrder)
                    && pagingArgs.SortOrder.ToUpper() == "ASC"
                    ? "ASC"
                    : "DESC";

                query = query.OrderBy(
                    String.Format("{0} {1}", 
                        pagingArgs.SortColumn, 
                        pagingArgs.SortOrder)
                    );
            }

            query = query
                .Skip(pagingArgs.PageIndex * pagingArgs.PageSize)
                .Take(pagingArgs.PageSize);

            var data = await query.ToListAsync();

            return new PagedList<T>(
                data, 
                count, 
                pagingArgs.PageIndex, 
                pagingArgs.PageSize,
                pagingArgs.SortColumn,
                pagingArgs.SortOrder);
        }

        /// <summary>
        /// Checks if the given property name exists to 
        /// protect against SQL injection acttacks.
        /// </summary>
        public static bool IsValidProperty(
            string propertyName, 
            bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // NOTE: This is not going to work for validating properties that exist on the derived types for my CaseForm's.
            //  Reflection here is only going to see the properties defined within the base, abstract CaseForm class.

            if (prop == null && throwExceptionIfNotFound)
                throw new NotSupportedException(
                    String.Format("ERROR: Property '{0} does not exist.", propertyName)
                    );

            return prop != null;
        }
        #endregion
    }
}
