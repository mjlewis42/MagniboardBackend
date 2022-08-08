using AutoMapper;
using MagniboardBackend.Data;
using MagniboardBackend.Data.DTO;
using MagniboardBackend.Data.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly MagniboardDbConnection _context;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(MagniboardDbConnection context, IMapper mapper, IConfiguration configuration, IUserService userService)
        {
            _context = context;
            this.mapper = mapper;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet("GetMe"), Authorize(Roles = "Admin")]
        public ActionResult<string> GetMe()
        {
            //option 1 (should probably start with other one and then switch to this)
            var userName = _userService.GetMyName();
            var role = User.FindFirstValue(ClaimTypes.Role).ToList();
            return Ok(new { userName, role});

            //option 2 (prevents a 'fat' controller with lots of logic and instead uses httpContext
            //this is the alternate way of doing this without all the dependency injection stuff but is bad practice?
            //ActionResult<object> instead of <string> as well
            //var userName = User?.Identity?.Name;
            //var userName2 = User.FindFirstValue(ClaimTypes.Name);
            //var role = User.FindFirstValue(ClaimTypes.Role);
            //return Ok(new {userName, userName2, role});
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserDTO request)
        {
            //Test if user already exists
            var userExists = await _context.User
                .FirstOrDefaultAsync(i => i.Username == request.Username);
            if(userExists != null)
            {
                Console.WriteLine("Username found");
                return StatusCode(500, "Username already exists!");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User()
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };


            var loginTemplate = mapper.Map<User>(user);
            await _context.AddAsync(loginTemplate);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] UserDTO request)
        {
            var userExists = await _context.User
                 .FirstOrDefaultAsync(i => i.Username == request.Username);
            if (userExists == null)
            {
                return BadRequest("Incorrect Username or Password (Username / take out later)");
            }

            if (!VerifyPasswordHash(request.Password, userExists.PasswordHash, userExists.PasswordSalt))
            {
                return BadRequest("Incorrect Username or Password (Password / take out later)");
            }

            var rolesNames = await _context.UserRole
                .Where(x => x.UserId == userExists.Id)
                .Select(y => y.Role.roleName)
                .ToListAsync();
            var roles = await _context.UserRole
                .Where(x => x.UserId == userExists.Id)
                .Select(y => y.Role.Id)
                .ToListAsync();
            //create a variable to return ids in the ok() instead of role names

            string token = CreateToken(userExists, rolesNames);

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);

            var username = userExists.Username;

            return Ok(new {token, username, roles});
        }

        /*[HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }*/

        /* private RefreshToken GenerateRefreshToken()
         {
             var refreshToken = new RefreshToken
             {
                 Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                 Expires = DateTime.Now.AddDays(7),
                 Created = DateTime.Now
             };

             return refreshToken;
         }*/

        /*private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }*/

        private string CreateToken(User user, List<string> userRoles)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }

}
