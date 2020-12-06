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
    [Table("UserProfileReviewLikes")]
    public class UserProfileReviewLike : FullAuditedEntity<long>
    {
        public long UserProfileReviewId { get; set; }
        [ForeignKey("UserProfileReviewId")]
        public UserProfileReview UserProfileReviewFk { get; set; }

        public long UserId { get; set; }
    }
}
