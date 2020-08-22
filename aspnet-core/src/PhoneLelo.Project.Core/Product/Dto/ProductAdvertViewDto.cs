using System;
using System.Collections.Generic;

namespace PhoneLelo.Project.Product.Dto
{
    public interface IProductAdvertViewDto
    {
        public string ProductCompanyName { get; set; }
        public string ProductModelName { get; set; }
        public int Views { get; set; }
    }

    public class ProductAdvertViewDto : IProductAdvertViewDto
    {
        public string ProductCompanyName { get; set; }
        public string ProductModelName { get; set; }
        public long ProductModelId { get; set; }
        public int Storage { get; set; }
        public int Ram { get; set; }
        public bool IsNew { get; set; }
        public bool IsPtaApproved { get; set; }
        public int Views { get; set; }
        public decimal Price { get; set; }
        public string PrimaryProductImage { get; set; }
        public DateTime AdvertPostedDate { get; set; }
       }

    public class ProductAdvertDetailViewDto : ProductAdvertDto, IProductAdvertViewDto
    {
        public string ProductCompanyName { get; set; }
        public string ProductModelName { get; set; }
        public int Views { get; set; }

        public List<ProductAdvertImageDto> Images { get; set; }
        public List<ProductAdvertBatteryUsageDto> ProductAdvertBatteryUsages { get; set; }
    }
}
