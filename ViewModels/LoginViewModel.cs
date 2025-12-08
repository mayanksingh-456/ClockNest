namespace ClockNest.ViewModels
{
    public class LoginViewModel
    {
       
        public string? Username { get; set; }

        public string? Password { get; set; }
        public bool HasAccountLockedError { get; set; } = false;

    }
}
