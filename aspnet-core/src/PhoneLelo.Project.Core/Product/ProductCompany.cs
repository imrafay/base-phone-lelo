using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductCompanies")]
    public class ProductCompany : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string DisplayName{ get; set; }
        public ProductTypeEnum ProductType { get; set; }
    }
}
