using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.Product.Dto
{
    [AutoMapFrom(typeof(UserProfileReview))]
    public class UserProfileReviewInputDto
    {
        public long Id { get; set; }
        public long ReviewerId { get; set; }
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
        public UserProfileReviewsStatsDto Rating { get; set; }
        public List<UserProfileReviewOutputListDto> UserProfileReviewOutputList { get; set; }
    }

    public class UserProfileReviewsStatsDto
    {
        public int TotalReviews { get; set; }
        public List<UserProfileReviewsRatingDto> Ratings { get; set; }
    }

    public class UserProfileReviewsRatingDto
    {
        public int Rating { get; set; }
        public int Count { get; set; }
    }

    public class UserProfileReviewOutputListDto
    {
        public long Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public string ReviewerFullName { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreationTime { get; set; }
    }

    [AutoMapFrom(typeof(UserProfileReview))]
    public class UserProfileReviewDto
    {
        public long Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }

    public class UserProfileReviewLikeDto
    {
        public long ReviewId { get; set; }
        public bool Status { get; set; }
    }
}
