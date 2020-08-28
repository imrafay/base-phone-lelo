using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("UserProductStores")]
    public class UserProductStore : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User UserFk { get; set; }

        public long ProductStoreId { get; set; }
        [ForeignKey("ProductStoreId")]
        public ProductStore ProductStoreFk { get; set; }
    }
}
