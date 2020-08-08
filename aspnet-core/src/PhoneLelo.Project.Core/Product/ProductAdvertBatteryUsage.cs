using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductAdvertBatteryUsages")]
    public class ProductAdvertBatteryUsage : FullAuditedEntity<long>
    {
        public long ProductAdvertId { get; set; }
        [ForeignKey("ProductAdvertId")]
        public ProductAdvert ProductAdvertFk { get; set; }

        public decimal Hours { get; set; }
        public ProductBatteryUsageEnum BatteryUsageType { get; set; }

    }
}
