namespace ClockNest.Models.Login
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? RedirectAction { get; set; }
       // public User? User { get; set; }
    }
}
