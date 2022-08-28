using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Currency_Calculator.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public UserRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<string?> LoginUser(UserDto userDto)
        {
            var result = await appDbContext.User.
                FirstOrDefaultAsync(u => u.Username == userDto.Username);
            if (result != null && result.PasswordHash.SequenceEqual(CalculatePasswordHash(userDto.Password, result.PasswordSalt)))
            {
                return CreateToken(result);
            }
            return null;
        }

        public async Task RegisterUser(UserDto userDto)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashAndSalt(userDto.Password, out passwordHash, out passwordSalt);
            await appDbContext.User.AddAsync(new User() { Username = userDto.Username, PasswordHash = passwordHash, PasswordSalt = passwordSalt });
            await appDbContext.SaveChangesAsync();
        }

        private static byte[] CalculatePasswordHash(string password, byte[] salt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(salt))
            {
                return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static void CreatePasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Appsettings:Token").Value));
            var credentialss = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(3), signingCredentials: credentialss); //TODO: move expiration value to config file
            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
