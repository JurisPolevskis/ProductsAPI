namespace ProductsAPI.Authentication.Dtos
{
    public record User
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
