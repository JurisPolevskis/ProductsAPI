using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Controllers;
using ProductsAPI.Database;

namespace ProductsAPI.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IHashingService hashingService;
        private readonly IProductDBContext dbContext;

        public AuthController(IHashingService hashingService, IProductDBContext dbContext)
        {
            this.hashingService = hashingService;
            this.dbContext = dbContext;
        }

        [HttpPost("create")]
        public IActionResult Create(Dtos.User user)
        {
            if (!UsernameValid(user.UserName))
                return BadRequest($"Invalid username: {user.UserName}");

            if (!PasswordValid(user.Password))
                return BadRequest($"Invalid password");

            if (UserExists(user, dbContext))
                return Conflict("User already exists");

            InsertUser(user, dbContext, hashingService);
            return Created("auth/create", user);
        }


        private static bool PasswordValid(string? password)
        {
            return !string.IsNullOrEmpty(password);
        }

        private static bool UsernameValid(string? userName)
        {
            return !string.IsNullOrEmpty(userName);
        }

        private static void InsertUser(Dtos.User user, IProductDBContext dbContext, IHashingService hashingService)
        {
            if (user.UserName is null)
                throw new ArgumentNullException(nameof(user), "user.UserName is null on insert when it should have been checked previously");
            if (user.Password is null)
                throw new ArgumentNullException(nameof(user), "user.Password is null on insert when it should have been checked previously");

            var dbUser = new DbObjects.User
            { 
                Username = user.UserName,
                Hash = hashingService.Hash(user.Password)
            };
            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();
        }

        private static bool UserExists(Dtos.User user, IProductDBContext dbContext)
        {
            return dbContext.Users.Any(dbUser => user.UserName == dbUser.Username);
        }
    }
}
