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

        public UserProfileReviewManager(
            IRepository<UserProfileReview, long> userProfileReviewRepository)
        {
            _userProfileReviewRepository = userProfileReviewRepository;
        }

        public async Task<UserProfileReview> GetByIdAsync(long id)
        {
            var userProfileReview =  await _userProfileReviewRepository.GetAll()
                .Include(x => x.ReviewerFk)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return userProfileReview;
        }

        public IQueryable<UserProfileReview> GetAllQuery(UserProfileReviewFilterDto filter)
        {
            var query = _userProfileReviewRepository.GetAll()
                .Include(x => x.ReviewerFk)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.UserId > 0, x => x.UserId == filter.UserId);

            return query;
        }

            public async Task<UserProfileReviewOutputDto> GetUserReviewsAsync(UserProfileReviewFilterDto filter)
        {
            var query = _userProfileReviewRepository.GetAll()
                .Include(x=>x.ReviewerFk)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .Where(x => x.UserId == filter.UserId);

            var userProfileReviews = await query              
                .Select(x => new UserProfileReviewOutputListDto()
                {
                    Id = x.Id,
                    CreationTime = x.CreationTime,
                    Rating = x.Rating,
                    Review = x.Review,
                    ReviewerFullName = x.ReviewerFk.FullName
                }).ToListAsync();

            var averageRating =userProfileReviews
                    .Select(x => x.Rating)
                    .Average();

            return new UserProfileReviewOutputDto()
            {
                AverageRating=averageRating,
                UserProfileReviewOutputList = userProfileReviews
            };
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
            await _userProfileReviewRepository.DeleteAsync(x=>x.Id == id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
