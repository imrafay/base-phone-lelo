using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductAdvertAccessories")]
    public class ProductAdvertAccessory : FullAuditedEntity<long>
    {
        public long ProductAdvertId { get; set; }
        [ForeignKey("ProductAdvertId")]
        public ProductAdvert ProductAdvertFk { get; set; }

        public ProductAccessoryEnum AccessoryType { get; set; }

    }
}
