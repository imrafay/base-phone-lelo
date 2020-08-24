using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    public class ProductAdvertInputDto
    {
        public ProductAdvertDto ProductAdvertinput{ get; set; }
        public List<ProductAdvertImageDto> Images { get; set; }
        public List<ProductAdvertBatteryUsageDto> ProductAdvertBatteryUsages { get; set; }
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
}