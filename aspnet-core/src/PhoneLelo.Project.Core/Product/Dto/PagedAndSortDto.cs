using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    public class PagedAndSortDto
    {    
        public int Id { get; set; }
        public int Name { get; set; }
        public SortByEnum SortBy { get; set; }
    }
}
