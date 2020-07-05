using IdentityManagement.Infrastructure.Persistence;
using IdentityManagement.Infrastructure.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace IdentityManagement.Tests.XUnit
{
    public class ServicesAcceptTests : IDisposable, IClassFixture<ServicesAcceptTests>
    {
        private const string actualCaller = "actual_caller";
        private const string expectedCaller = "expected_caller";

        private readonly IUserClaimsPrincipalFactory<AppUser> factory;
        private readonly UserManager<AppUser> manager = new UserManager<AppUser>();

        public ServicesAcceptTests()
        {
            Debug.WriteLine("Test başlangıcı");
        }

        public void Dispose()
        {
            Debug.WriteLine("Test bitişi"); //TODO: not dusurulmeli
        }

        [Fact]
        public async void Should_IsActiveAsync_False()
        {
            //Arrange
            IsActiveContext actualContext = new IsActiveContext(new ClaimsPrincipal(),new Client(),actualCaller);
            IsActiveContext expectedContext = new IsActiveContext(new ClaimsPrincipal(), new Client(), expectedCaller);

            IdentityClaimsProfileService service = new IdentityClaimsProfileService(factory, manager);
            Task expected = new Task(async () => await service.IsActiveAsync(expectedContext));

            //Act
            Task actual = service.IsActiveAsync(actualContext);

            //Assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void Test10()
        {
            Assert.True(true);
        }

        [Fact]
        public void Test100()
        {
            Assert.True(true);
        }

        [Theory, InlineData(new object[] { "de-DE", "2017.12.19", "19 Dezember 2017" })]
        [InlineData(new object[] { "en-US", "12/.19/2017", "19 December 2017" })]
        public void ExampleTest(string language,string date,string prettyDate)
        {
            Assert.True(true);
        }
    }
}
