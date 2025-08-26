using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Authentication.Definitions;
using ProductsAPI.Database;

namespace ProductsAPI.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IHashingService hashingService, IProductDBContext dbContext, ITokenService tokenService) : ControllerBase
    {
        private readonly IHashingService hashingService = hashingService;
        private readonly IProductDBContext dbContext = dbContext;

        [HttpPost("create")]
        [Authorize(Policy = Policies.UserManagement)]
        public IActionResult Create(Dtos.User user)
        {
            if (!UsernameValid(user.Username))
                return BadRequest($"Invalid username: {user.Username}");

            if (!PasswordValid(user.Password))
                return BadRequest($"Invalid password: {user.Password}");

            if (UserExists(user, dbContext))
                return Conflict("User already exists");

            InsertUser(user);
            return Created("auth/create", user);
        }

        [HttpPost("login")]
        public IActionResult Login(Dtos.User user)
        {
            if (!UsernameValid(user.Username))
                return BadRequest($"Invalid username: {user.Username}");

            if (!PasswordValid(user.Password))
                return BadRequest($"Invalid password: {user.Password}");

            var validDbUser = GetValidUser(user);
            if (validDbUser is null)
                return BadRequest("Invalid username/password combination");


            return new ObjectResult(tokenService.Generate(validDbUser));
        }

        private void InsertUser(Dtos.User user)
        {
            if (user.Username is null)
                throw new ArgumentNullException(nameof(user), "user.UserName is null on insert when it should have been checked previously");
            if (user.Password is null)
                throw new ArgumentNullException(nameof(user), "user.Password is null on insert when it should have been checked previously");

            var dbUser = new DbObjects.User
            {
                Username = user.Username,
                Hash = hashingService.Hash(user.Password)
            };
            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges(HttpContext);
        }

        private DbObjects.User? GetValidUser(Dtos.User user)
        {
            var foundUser = dbContext.Users.FirstOrDefault(existingUser => user.Username == existingUser.Username);
            if (foundUser is null || user.Password == null)
                return null;
            if (!hashingService.Verify(user.Password, foundUser.Hash))
                return null;
            return foundUser;
        }

        private static bool PasswordValid(string? password)
        {
            return !string.IsNullOrEmpty(password);
        }

        private static bool UsernameValid(string? userName)
        {
            return !string.IsNullOrEmpty(userName);
        }

        private static bool UserExists(Dtos.User user, IProductDBContext dbContext)
        {
            return dbContext.Users.Any(dbUser => user.Username == dbUser.Username);
        }
    }
}
