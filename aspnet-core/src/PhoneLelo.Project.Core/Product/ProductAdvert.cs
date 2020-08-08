using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductAdverts")]
    public class ProductAdvert : FullAuditedEntity<long>
    {
        public long ProductModelId { get; set; }
        [ForeignKey("ProductModelId")]
        public ProductModel ProductModelFk { get; set; }

        public int Storage { get; set; }
        public int Ram { get; set; }
        public bool IsNew { get; set; }
        public bool IsPtaApproved { get; set; }
        public bool IsExchangeable { get; set; }      
        public decimal Price { get; set; }
        public bool IsNegotiable{ get; set; }
        public decimal? NegotiableMinValue { get; set; }
        public decimal? NegotiableMaxValue { get; set; }

        public bool? IsSpot{ get; set; }
        public bool? IsDamage{ get; set; }
        public bool? IsFingerSensorWorking{ get; set; }
        public bool? IsFaceSensorWorking{ get; set; }
        public int? BatteryHealth{ get; set; }
        public string  Description { get; set; }
    }
}
