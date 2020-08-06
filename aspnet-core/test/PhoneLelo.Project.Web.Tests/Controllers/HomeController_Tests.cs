using System.Threading.Tasks;
using PhoneLelo.Project.Models.TokenAuth;
using PhoneLelo.Project.Web.Controllers;
using Shouldly;
using Xunit;

namespace PhoneLelo.Project.Web.Tests.Controllers
{
    public class HomeController_Tests: ProjectWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}