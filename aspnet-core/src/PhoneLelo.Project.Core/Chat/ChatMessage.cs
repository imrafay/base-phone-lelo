using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Location;
using PhoneLelo.Project.Product.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project.Chat
{
    [Table("ChatMessages")]
    public class ChatMessage : FullAuditedEntity<long>
    {
        public long ReceiverId { get; set; }

        public long SenderId { get; set; }

        public string Message{ get; set; }
        public MessageStatusEnum MessageStatus { get; set; }

    }
}
