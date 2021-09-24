using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeatherApp.Domain;
using WeatherApp.Domain.Dtos;

namespace WeatherApp.Services.User
{
    public class UserService : IUserService
    {
        private readonly List<Domain.Dtos.User> _users = new()
        {
            new Domain.Dtos.User
            {
                Id = 1,
                FirstName = "Guest",
                LastName = "",
                Username = "guest",
                Password = "guest"
            },
            new Domain.Dtos.User
            {
                Id = 2,
                FirstName = "Pierre",
                LastName = "Roux",
                Username = "Pierre",
                Password = "pierre"
            },
            new Domain.Dtos.User
            {
                Id = 3,
                FirstName = "Pierre",
                LastName = "Roux",
                Username = "Blaarkies",
                Password = "pierre"
            }
        };

        private readonly Settings _settings;

        public UserService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public AuthenticateResponse Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user is null)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public Domain.Dtos.User GetById(int id)
        {
            return _users.SingleOrDefault(u => u.Id == id);
        }

        private string GenerateJwtToken(Domain.Dtos.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
