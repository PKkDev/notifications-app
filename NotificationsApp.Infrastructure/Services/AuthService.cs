using EfData.Context;
using EfData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NotificationsApp.Domain.DTO.Authorize;
using NotificationsApp.Domain.ServicesContract;
using NotificationsApp.Infrastructure.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly ApplicationContext _context;

        public AuthService(
            ILogger<AuthService> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<LoginResponseDto> AuthorizeUser(
            string userName, string password, CancellationToken ct)
        {
            User user = await _context.User
               .FirstOrDefaultAsync(x => x.UserName == userName, ct);

            if (user == null)
                throw new Exception("пользователь не найден");

            string token = await CreateTokenAsync(userName, ct);
            int id = user.Id;
            string role = user.Role;
            string name = $"{user.FName} {user.SName}";

            var result = new LoginResponseDto(id, token, role, name);
            return result;
        }

        public async Task<LoginResponseDto> RegisterUser(
            string userName, string password, string email, CancellationToken ct)
        {
            User newUser = new User()
            {
                UserName = userName,
                Password = password,
                Email = email,
                Role = "user",
                Phone = GeneratePhone(),
                FName = GenerateName(5),
                SName = GenerateName(7)
            };

            await _context.User.AddAsync(newUser, ct);
            await _context.SaveChangesAsync(ct);

            return await AuthorizeUser(userName, password, ct);
        }

        private async Task<string> CreateTokenAsync(string login, CancellationToken ct)
        {
            User user = await _context.User
                .FirstOrDefaultAsync(x => x.UserName == login, ct);

            var identity = GetIdentity(login);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(string login)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, login));

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        private string GeneratePhone()
        {
            Random r = new Random();
            string phone = "8";
            int a = 1;
            while (a < 11)
            {
                phone += r.Next(9);
                a++;
            }
            return phone;
        }
    }
}
