using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using PhoneLelo.Project.Authorization.Users;

namespace PhoneLelo.Project.Store.Dto
{
    public class StoreInputDto
    {
        public long UserId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        public string ImageIconUrl { get; set; }

    }
}
