using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoListMauiApp.Args
{
    public class PunishmentPointValueChangedEventArgs : EventArgs
    {
        private readonly int _value;

        private readonly string _message;
        public int Value { get { return _value; } }
        public string Message { get { return _message;} }
        public PunishmentPointValueChangedEventArgs(string messege, int value)
        {
            _message = messege;
            _value = value;
        }
    }
}
