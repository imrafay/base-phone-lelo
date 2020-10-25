using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Location;
using PhoneLelo.Project.Product.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("UserProfileReviews")]
    public class UserProfileReview : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User UserFk { get; set; }

        public long? ReviewerId { get; set; }
        [ForeignKey("ReviewerId")]
        public User ReviewerFk { get; set; }

        public long? ProductStoreId { get; set; }
        [ForeignKey("ProductStoreId")]
        public ProductStore ProductStoreFk { get; set; }

        public string Review { get; set; }
        public int Rating { get; set; }
        public string GuestEmailAddress { get; set; }
        public string GuestFirstName{ get; set; }
        public string GuestLastName{ get; set; }

    }
}
