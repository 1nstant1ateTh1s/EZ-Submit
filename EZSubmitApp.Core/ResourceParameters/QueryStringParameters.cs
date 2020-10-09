
namespace EZSubmitApp.Core.ResourceParameters
{
    public abstract class QueryStringParameters
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = null;
        public string SortOrder { get; set; } = null;
    }
}
