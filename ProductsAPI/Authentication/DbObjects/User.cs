using ProductsAPI.Authentication.Definitions;

namespace ProductsAPI.Authentication.DbObjects
{
    public record User
    {
        public int ID { get; set; }
        public required string Username { get; set; }
        public required string Hash { get; set; }
        public string? Role { get; set; } = Roles.User;
    }
}
