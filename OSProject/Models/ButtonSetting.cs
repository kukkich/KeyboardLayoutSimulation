using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public ButtonSetting(KeyboardButton button, char? settedValue) =>
            (Button, SettedValue) = (button, settedValue);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
