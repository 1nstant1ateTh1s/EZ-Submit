using EZSubmitApp.Core.Paging;

namespace EZSubmitApp.Requests
{
    public class PageSearchRequest
    {
        //public PageSearchArgs Args { get; set; }

        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
