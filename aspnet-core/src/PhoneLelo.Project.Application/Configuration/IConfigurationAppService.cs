using System.Threading.Tasks;
using PhoneLelo.Project.Configuration.Dto;

namespace PhoneLelo.Project.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
