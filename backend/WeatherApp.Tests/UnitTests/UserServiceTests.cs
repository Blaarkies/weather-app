using Microsoft.Extensions.Options;
using NUnit.Framework;
using WeatherApp.Domain;
using WeatherApp.Services.User;

namespace WeatherApp.Tests.UnitTests
{
    public class UserServiceTests
    {
        private static UserService CreateService()
        {
            var mockSettings = Options.Create(new Settings
            {
                PageSize = 20,
                JwtKey = "my-key-lorem-ipsum"
            });

            return new UserService(mockSettings);
        }

        [TestCase("name", "password", false)]
        [TestCase("Pierre", "pierre", true)]
        [TestCase("", "pierre", false)]
        [TestCase("Pierre", "", false)]
        [TestCase("", "", false)]
        public void Authenticate_WhenWithArguments_ReturnsExpectedResults(
            string username,
            string password,
            bool shouldBeAuthentic)
        {
            var service = CreateService();

            var authResponse = service.Authenticate(username, password);
            var isAuthenticated = authResponse is not null;

            Assert.AreEqual(shouldBeAuthentic, isAuthenticated);
        }

        [Test]
        public void Authenticate_WhenValidUser_ReturnsAuthResponse()
        {
            var service = CreateService();

            var authResponse = service.Authenticate("Pierre", "pierre");

            Assert.IsNotNull(authResponse.Token);
        }

        [Test]
        public void Authenticate_WhenInvalidUser_ReturnsNull()
        {
            var service = CreateService();

            var authResponse = service.Authenticate("not", "real");

            Assert.IsNull(authResponse);
        }

        [Test]
        public void GetById_WhenWrongId_ReturnsNull()
        {
            var service = CreateService();

            var user = service.GetById(-1);

            Assert.IsNull(user);
        }

        [Test]
        public void GetById_WhenCorrectId_ReturnsCorrectUser()
        {
            var service = CreateService();

            var user = service.GetById(1);

            Assert.IsNotNull(user);
            Assert.AreEqual("Pierre", user.Username);
        }
    }
}
