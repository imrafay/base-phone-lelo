using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductAdvertViewLogs")]
    public class ProductAdvertViewLog : FullAuditedEntity<long>
    {
        public long ProductAdvertId { get; set; }
        [ForeignKey("ProductAdvertId")]
        public ProductAdvert ProductAdvertFk { get; set; }
       
        public long? UserId{ get; set; }

    }
}
