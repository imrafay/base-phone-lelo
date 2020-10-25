using AutoMapper;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Product.Dto;

namespace PhoneLelo.Project.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
    
    
    public class ProductAdvertProfile : Profile
    {
        public ProductAdvertProfile()
        {
            CreateMap<ProductAdvertDto, ProductAdvert>();
        }
    }    
    
    public class ProductAdvertBatteryUsageProfile : Profile
    {
        public ProductAdvertBatteryUsageProfile()
        {
            CreateMap<ProductAdvertBatteryUsageDto, ProductAdvertBatteryUsage>();
        }
    }    
    
    public class ProductAdvertImageProfile : Profile
    {
        public ProductAdvertImageProfile()
        {
            CreateMap<ProductAdvertImageDto, ProductAdvertImage>();
        }
    }


    public class ProductModeleProfile : Profile
    {
        public ProductModeleProfile()
        {
            CreateMap<ProductModelDto, ProductModel>();
        }
    }

    public class UserProfileReviewProfile : Profile
    {
        public UserProfileReviewProfile()
        {
            CreateMap<UserProfileReviewDto, UserProfileReview>();
        }
    }
}
