using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.Models
{
    public class ButtonSetting : INotifyPropertyChanged
    {
        public KeyboardButton Button { get; set; }
        public char? SettedValue 
        { 
            get => _settedValue;
            set
            {
                _settedValue = value;
                OnPropertyChanged(nameof(SettedValue));
            }
        }

        private char? _settedValue;

        public ButtonSetting(KeyboardButton button, char? settedValue)
        {
            (Button, SettedValue) = (button, settedValue);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
