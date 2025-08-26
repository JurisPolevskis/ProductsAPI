using ProductsAPI.Authentication.DbObjects;

namespace ProductsAPI.Authentication
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}