using System.Collections.Generic;

namespace PhoneLelo.Project.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
