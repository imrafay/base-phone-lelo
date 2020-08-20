using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace PhoneLelo.Project.Models.TokenAuth
{
    public class AuthenticateModel
    {
        //TODO: Remove comments
        //[Required]
        //[StringLength(AbpUserBase.MaxEmailAddressLength)]
        //public string UserNameOrEmailAddress { get; set; }

        //[Required]
        //[StringLength(AbpUserBase.MaxPlainPasswordLength)]
        //public string Password { get; set; }
        
        [Required]
        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxEmailConfirmationCodeLength)]
        public string Code { get; set; }
        
        public bool RememberClient { get; set; }
    }
}
