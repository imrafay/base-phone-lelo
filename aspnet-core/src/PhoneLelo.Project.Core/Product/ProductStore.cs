using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductStores")]
    public class ProductStore : FullAuditedEntity<long>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string StoreCode { get; set; }

        public bool IsActive { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

    }
}
