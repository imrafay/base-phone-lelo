using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductStores")]
    public class ProductStore : FullAuditedEntity<long>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Guid StoreCode { get; set; }

        public bool IsActive { get; set; }

        public string Address { get; set; }
        public string ImageIconUrl { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public ICollection<UserProductStore> UserProductStores { get; set; }
    }
}
