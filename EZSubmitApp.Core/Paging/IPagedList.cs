using System.Collections.Generic;

namespace EZSubmitApp.Core.Paging
{
    public interface IPagedList<out T>
    {
        IEnumerable<T> Data { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        string SortColumn { get; }
        string SortOrder { get; }
    }
}
