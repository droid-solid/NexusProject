namespace ResolveIQ.WebUI.Services.Auth
{
    public interface IAuthenticationService
    {
        bool Login(string username, string password);  
    }
}
