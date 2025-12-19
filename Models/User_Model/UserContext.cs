using System.Security.Claims;

namespace ClockNest.Models.User_Model
{
    public class UserContext
    {
        public string? UserId { get; set; }
        public ClaimsPrincipal? Principal { get; set; }
    }
}
