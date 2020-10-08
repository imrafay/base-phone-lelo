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
        public long Id { get; set; }
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

        public string UserFullName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighbourhood { get; set; }
        public DateTime CreationTime { get; set; }

    }

    public class ProductAdvertDetailViewDto : IProductAdvertViewDto
    {
        public string ProductCompanyName { get; set; }
        public string ProductModelName { get; set; }
        public int Views { get; set; }
        public ProductAdvertDto ProductAdvert { get; set; }
        public List<ProductAdvertImageDto> Images { get; set; }
        public List<ProductAdvertBatteryUsageDto> ProductAdvertBatteryUsages { get; set; }
        public List<ProductAdvertAccessoryDto> productAdvertAccessories { get; set; }
    }
}
