using FluentAssertions;
using Infrastructure.Services.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit.Authentification
{
    public class AuthentificationServiceTest
    {
        [Fact]
        public void TryGetClaimPrincipal_InvalidUserNameAndPassword_ReturnsNull()
        {

            var service = new AuthentificationService(new Domain.Models.Settings.ApplicationSettingsModel()
            {
                Login = "1",
                Password = "2"
            });

            service.TryGetClaimPrincipal("5","1").Should().BeNull();
        }

        [Theory]
        [InlineData("Alex")]
        [InlineData("ALEX")]
        [InlineData("alex")]
        public void TryGetClaimPrincipal_LoginCaseInsencitive_Success(string login)
        {

            var service = new AuthentificationService(new Domain.Models.Settings.ApplicationSettingsModel()
            {
                Login = "alex",
                Password = "2"
            });

            service.TryGetClaimPrincipal(login, "2").Should().NotBeNull();
        }

        [Fact]
        public void TryGetClaimPrincipal_SuccessfullCall_CreatedPrincipalInstanceIsCached()
        {

            var service = new AuthentificationService(new Domain.Models.Settings.ApplicationSettingsModel()
            {
                Login = "1",
                Password = "2"
            });
            Assert.True(object.ReferenceEquals(service.TryGetClaimPrincipal("1", "2"), service.TryGetClaimPrincipal("1", "2")));
        }
    }
}
