
namespace Project.Services.Utilities
{
    public class PagingParameters
    {
        public int? pageNumber { get; set; }
        public int pageSize { get; set; }
        public string currentFilter { get; set; }
    }
}
