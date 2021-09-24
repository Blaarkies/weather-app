using WeatherApp.Domain.Dtos;

namespace WeatherApp.Services.User
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(string username, string password);
        Domain.Dtos.User GetById(int id);
    }
}
