using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    public class PagedAndSortDto
    {    
        public int Page { get; set; }
        public int PageSize { get; set; }
        public SortByEnum SortBy { get; set; }
    }
}
