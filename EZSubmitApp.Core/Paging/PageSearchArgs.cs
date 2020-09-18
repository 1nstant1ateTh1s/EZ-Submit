using System.Collections.Generic;

namespace EZSubmitApp.Core.Paging
{
    public class PageSearchArgs
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public PagingStrategy PagingStrategy { get; set; }
        public List<SortingOption> SortingOptions { get; set; }
        public List<FilteringOption> FilteringOptions { get; set; }
    }
}
