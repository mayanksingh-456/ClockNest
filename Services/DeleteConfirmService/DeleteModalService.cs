namespace ClockNest.Services.DeleteConfirmService
{
    public class DeleteModalService
    {
        public bool IsVisible { get; private set; }
        public string Message { get; private set; } = "";

        private Func<Task>? _onConfirm;

        public event Action? OnChange;

        public void Show(string message, Func<Task> onConfirm)
        {
            Message = message;
            _onConfirm = onConfirm;
            IsVisible = true;
            NotifyStateChanged();
        }

        public async Task Confirm()
        {
            IsVisible = false;
            NotifyStateChanged();

            if (_onConfirm != null)
                await _onConfirm();
        }

        public void Close()
        {
            IsVisible = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
