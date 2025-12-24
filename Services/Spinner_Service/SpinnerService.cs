namespace ClockNest.Services.Spinner_Service
{
    public class SpinnerService
    {
        public event Action? OnShow;
        public event Action? OnHide;

        public string? Message { get; private set; }

        public void Show(string? message = null)
        {
            Message = message;
            OnShow?.Invoke();
        }

        public void Hide()
        {
            Message = null;
            OnHide?.Invoke();
        }
    }
}
