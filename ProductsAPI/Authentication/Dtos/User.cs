namespace ProductsAPI.Authentication.Dtos
{
    public record User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
