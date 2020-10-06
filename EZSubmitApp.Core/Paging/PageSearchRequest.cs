
namespace EZSubmitApp.Core.Paging
{
    public class PageSearchRequest
    {
        //public PageSearchArgs Args { get; set; }

        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = null;
        public string SortOrder { get; set; } = null;
    }
}
