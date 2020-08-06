using Abp.AutoMapper;
using PhoneLelo.Project.Authentication.External;

namespace PhoneLelo.Project.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
