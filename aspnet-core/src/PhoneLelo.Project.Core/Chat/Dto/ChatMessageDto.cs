using Abp.AutoMapper;
using PhoneLelo.Project.Chat;
using PhoneLelo.Project.Chat.Enum;
using PhoneLelo.Project.Product.Dto;
using System;

namespace PhoneLelo.Project.Chat.Dto
{
    public class ChatMessageDto
    {    
        public long SenderId { get; set; }
        public string SenderName { get; set; }

        public long ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Message{ get; set; }

        public MessageStatusEnum MessageStatus { get; set; }
        public DateTime Date { get; set; }
    }

    [AutoMapFrom(typeof(ChatMessage))]
    public class ChatMessageInputDto
    {
        public long ReceiverId { get; set; }

        public long SenderId { get; set; }

        public string Message { get; set; }
    }

    public class ChatMessageFilterInputDto
    {
        public long ReceiverId { get; set; }

        public long SenderId { get; set; }

        public PagedAndSortChatFilterDto pagedAndSort { get; set; }
    }

    public class UserChatOutputDto
    {
        public long SenderId { get; set; }
        public string SenderName { get; set; }

        public string LastMessage { get; set; }

        public MessageStatusEnum MessageStatus { get; set; }
        public DateTime Date { get; set; }
    }

}
