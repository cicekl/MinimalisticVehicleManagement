
namespace Project.Services.Utilities
{
    public class PagingParameters
    {
        public int? PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CurrentFilter { get; set; }
    }
}
