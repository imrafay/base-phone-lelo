using PhoneLelo.Project.Product.Enum;
using System.Collections.Generic;

namespace PhoneLelo.Project.Product.Dto
{
    public class ProductAdvertFilterInputDto
    {
        public ProductAdvertFilterInputDto()
        {
            NameFilter = NameFilter.ToLower();
        }

        public long? ProductModelId { get; set; }
        public long? ProductCompanyId { get; set; }
        public string NameFilter { get; set; }
        public List<int> RamFilter{ get; set; }
        public List<int> StorageFilter{ get; set; }

        public bool? IsNew { get; set; }
        public bool? IsPtaApproved { get; set; }
        public bool? IsExchangeable { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsNegotiable { get; set; }

        public bool? IsSpot { get; set; }
        public bool? IsDamage { get; set; }
        public SortByEnum SortBy { get; set; }


    }
    
}
