
namespace ClockNest.Services.Toast_Service
{
    public class ToastService
    {
        public event Action<string, ToastType>? OnShow;

        public void Show(string message, ToastType type = ToastType.Info)
        {
            OnShow?.Invoke(message, type);
        }
    }

    public enum ToastType
    {
        Success,
        Error,
        Warning,
        Info
    }
}
