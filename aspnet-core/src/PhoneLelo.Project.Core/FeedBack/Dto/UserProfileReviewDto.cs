using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    public class UserProfileReviewInputDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }

    public class UserProfileReviewFilterDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
    }

    public class UserProfileReviewOutputDto
    {
        public double AverageRating { get; set; }
        public List<UserProfileReviewOutputListDto> UserProfileReviewOutputList { get; set; }
    }
    public class UserProfileReviewOutputListDto
    {
        public long Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public string ReviewerFullName { get; set; }
        public DateTime CreationTime { get; set; }
    }

    [AutoMapFrom(typeof(UserProfileReview))]
    public class UserProfileReviewDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long? ReviewerId { get; set; }
        public long? ProductStoreId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}
