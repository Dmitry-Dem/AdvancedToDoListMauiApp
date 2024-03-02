namespace AdvancedToDoListMauiApp.Args
{
    public class PunishmentPointValueChangedEventArgs : EventArgs
    {
        private readonly int _currentValue;

        private readonly int _decreaseValue;

        private readonly string _message;
        public int Value { get { return _currentValue; } }
        public int DecreaseValue { get { return _decreaseValue; } }
        public string Message { get { return _message;} }
        public PunishmentPointValueChangedEventArgs(string messege, int currentValue, int decreaseValue)
        {
            _message = messege;
            _currentValue = currentValue;
            _decreaseValue = decreaseValue;
        }
    }
}
