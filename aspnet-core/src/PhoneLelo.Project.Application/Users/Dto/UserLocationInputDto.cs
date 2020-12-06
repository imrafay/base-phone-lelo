using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using PhoneLelo.Project.Authorization.Users;

namespace PhoneLelo.Project.Users.Dto
{
    public class UserLocationInputDto 
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long StateId { get; set; }

        [Required]
        public long CityId { get; set; }

        public long? NeighbourhoodId { get; set; }   
    }
}
