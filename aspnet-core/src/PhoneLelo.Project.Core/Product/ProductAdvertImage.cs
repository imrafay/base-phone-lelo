using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductAdvertImages")]
    public class ProductAdvertImage : FullAuditedEntity<long>
    {
        public long ProductAdvertId { get; set; }
        [ForeignKey("ProductAdvertId")]
        public ProductAdvert ProductAdvertFk { get; set; }

        public string Image { get; set; }
        public ProductImagePriorityEnum ImagePriority { get; set; }

    }
}
