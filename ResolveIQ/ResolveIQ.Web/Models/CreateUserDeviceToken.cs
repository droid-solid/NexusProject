namespace ResolveIQ.Web.Models
{
    public class CreateUserDeviceToken
    {
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceType { get; set; }
    }
}
