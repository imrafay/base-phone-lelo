using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    public class ProductAdvertInputDto
    {
        public ProductAdvertInputDto() 
        {
            ProductAdvertBatteryUsages = new List<ProductAdvertBatteryUsageDto>();
            Images = new List<ProductAdvertImageDto>();
            ProductAdvertAccessories = new List<ProductAdvertAccessoryDto>();
        }
        public ProductAdvertDto ProductAdvertinput{ get; set; }
        public List<ProductAdvertImageDto> Images { get; set; }
        public List<ProductAdvertBatteryUsageDto> ProductAdvertBatteryUsages { get; set; }
        public List<ProductAdvertAccessoryDto> ProductAdvertAccessories { get; set; }
    }

    [AutoMapFrom(typeof(ProductAdvert))]
    public class ProductAdvertDto : EntityDto<long>
    {
        public long ProductModelId { get; set; }
        public int Storage { get; set; }
        public int Ram { get; set; }
        public bool IsNew { get; set; }
        public bool IsPtaApproved { get; set; }
        public bool IsExchangeable { get; set; }
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
        public decimal? NegotiableMinValue { get; set; }
        public decimal? NegotiableMaxValue { get; set; }

        public bool? IsSpot { get; set; }
        public bool? IsDamage { get; set; }
        public bool? IsFingerSensorWorking { get; set; }
        public bool? IsFaceSensorWorking { get; set; }
        public int? BatteryHealth { get; set; }
        public string Description { get; set; }

        public bool? IsKit { get; set; }
        public bool? IsInWarranty { get; set; }
        public int? RemaingWarrantyInMonths { get; set; }
    }

    [AutoMapFrom(typeof(ProductAdvertBatteryUsage))]
    public class ProductAdvertBatteryUsageDto : EntityDto<long>
    {
        public decimal Hours { get; set; }
        public ProductBatteryUsageEnum BatteryUsageType { get; set; }

    }
    
    [AutoMapFrom(typeof(ProductAdvertImage))]
    public class ProductAdvertImageDto: EntityDto<long>
    {
        public string Image { get; set; }
        public ProductImagePriorityEnum ProductImagePriority { get; set; }

    }
    
    [AutoMapFrom(typeof(ProductAdvertAccessory))]
    public class ProductAdvertAccessoryDto : EntityDto<long>
    {
        public string AccessoryName { get; set; }
        public ProductAccessoryEnum AccessoryType { get; set; }
    }
}
