namespace Infrastructure.Authentication
{
    public interface IJWTHandler
    {
        string CreateToken(string login);
    }
}