using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Runtime.Session;
using Abp.UI;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Product.Dto;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class UserProfileReviewAppService : ApplicationService, IUserProfileReviewAppService
    {
        private readonly IAbpSession _abpSession; 
        private readonly UserProfileReviewManager _userProfileReviewManager;

        public UserProfileReviewAppService(

            IAbpSession abpSession,
            UserProfileReviewManager userProfileReviewManager)
        {
            _abpSession = abpSession;
            _userProfileReviewManager = userProfileReviewManager;
        }


        public async Task<UserProfileReviewOutputDto> GetUserReviews(UserProfileReviewFilterDto filter)
        {
            return await _userProfileReviewManager.GetUserReviewsAsync(filter);
        }

        public async Task Create(
            UserProfileReviewInputDto input)
        {
            if (input == null)
            {
                return;
            }

            var userProfileReview = ObjectMapper
                   .Map<UserProfileReview>(input);

            if (userProfileReview == null)
            {
                throw new UserFriendlyException(AppConsts.ErrorMessage.GeneralErrorMessage);
            }

            await _userProfileReviewManager.CreateAsync(
                userProfileReview: userProfileReview);
        }


        public async Task Update(
          UserProfileReviewDto input)
        {
            if (input.Id > 0)
            {
                throw new UserFriendlyException(AppConsts.ErrorMessage.IdMustBeProvided);
            }

            var userProfileReview = await _userProfileReviewManager.GetByIdAsync(input.Id);

            if (userProfileReview == null)
            {
                throw new UserFriendlyException(AppConsts.ErrorMessage.NotFound);
            }

             ObjectMapper.Map(input, userProfileReview); ;

            if (userProfileReview == null)
            {
                return;
            }

            await _userProfileReviewManager.UpdateAsync(
                userProfileReview: userProfileReview);
        }

        public async Task Delete(long id)
        {
            await _userProfileReviewManager.Delete(id);
        }
    }
}

