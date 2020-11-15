using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    public class PagedAndSortDto
    {    
        public int Page { get; set; }
        public int PageSize { get; set; }
        public SortByEnum SortBy { get; set; }
    }

    public class PagedAndSortFilterDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PagedAndSortChatFilterDto: PagedAndSortFilterDto
    {
        public SortByDateEnum SortBy { get; set; }
    }
}
