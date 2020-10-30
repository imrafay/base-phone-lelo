using Microsoft.AspNetCore.Identity;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.MultiTenancy;
using Abp.Domain.Services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Abp.EntityFrameworkCore.Repositories;
using PhoneLelo.Project.Product.Dto;
using System;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace PhoneLelo.Project.Authorization
{
    public class UserProfileReviewManager : DomainService
    {
        private readonly IRepository<UserProfileReview, long> _userProfileReviewRepository;
        private readonly IRepository<UserProfileReviewLike, long> _userProfileReviewLikeRepository;

        public UserProfileReviewManager(
            IRepository<UserProfileReview, long> userProfileReviewRepository, IRepository<UserProfileReviewLike, long> userProfileReviewLikeRepository)
        {
            _userProfileReviewRepository = userProfileReviewRepository;
            _userProfileReviewLikeRepository = userProfileReviewLikeRepository;
        }

        public async Task<UserProfileReview> GetByIdAsync(long id)
        {
            var userProfileReview = await _userProfileReviewRepository.GetAll()
                .Include(x => x.ReviewerFk)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return userProfileReview;
        }

        public IQueryable<UserProfileReview> GetAllQuery(UserProfileReviewFilterDto filter)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = _userProfileReviewRepository.GetAll()
                .Include(x => x.ReviewerFk)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.UserId > 0, x => x.UserId == filter.UserId);

                return query;
            }
        }

        public async Task<UserProfileReviewOutputDto> GetUserReviewsAsync(UserProfileReviewFilterDto filter)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = _userProfileReviewRepository.GetAll()
                .Include(x => x.ReviewerFk)
                .Include(x => x.userProfileReviewLikes)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .Where(x => x.UserId == filter.UserId);

                var userProfileReviews = await query
                    .Select(x => new UserProfileReviewOutputListDto()
                    {
                        Id = x.Id,
                        CreationTime = x.CreationTime,
                        Rating = x.Rating,
                        Review = x.Review,
                        ReviewerFullName = x.ReviewerFk.FullName,
                        LikeCount = x.userProfileReviewLikes.Count()
                    }).ToListAsync();

                if (userProfileReviews.Any())
                {
                    var averageRating = userProfileReviews
                        .Select(x => x.Rating)
                        .Average();

                    var ratings = userProfileReviews.GroupBy(
                            p => p.Rating,
                            (key, g) => new UserProfileReviewsRatingDto
                            {
                                Rating = key,
                                Count = g.Count()
                            }).ToList();

                    return new UserProfileReviewOutputDto()
                    {
                        AverageRating = averageRating,
                        Rating = new UserProfileReviewsStatsDto()
                        {
                            Ratings = ratings,
                            TotalReviews = userProfileReviews.Count()
                        },
                        UserProfileReviewOutputList = userProfileReviews
                    };
                }

                return new UserProfileReviewOutputDto();
            }
        }

        public async Task CreateAsync(UserProfileReview userProfileReview)
        {
            await _userProfileReviewRepository.InsertAsync(userProfileReview);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfileReview userProfileReview)
        {
            await _userProfileReviewRepository.UpdateAsync(userProfileReview);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            await _userProfileReviewRepository.DeleteAsync(x => x.Id == id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<UserProfileReviewLike> GetUserProfileReviewLikeByIdAsync(
            long reviewId,
            long userId)
        {
            var userProfileReviewLike = await _userProfileReviewLikeRepository.GetAll()
                .Include(x => x.UserProfileReviewFk)
                .Where(x => x.UserProfileReviewId == reviewId &&
                x.UserId == userId)
                .FirstOrDefaultAsync();

            return userProfileReviewLike;
        }


        public async Task CreateUserProfileReviewLike(
            UserProfileReviewLike input)
        {
            await _userProfileReviewLikeRepository.InsertAsync(input);
        }

        public async Task DeleteUserProfileReviewLike(
        long id)
        {
             await _userProfileReviewLikeRepository
                .DeleteAsync(x=>x.Id == id);
        }
    }
}
