using OSProject.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OSProject.ViewModels
{
    public class ButtonValueChangingViewModel : INotifyPropertyChanged
    {
        public string NewValue
        {
            get => _newValue;
            set
            {
                if (value.Length < 2)
                {
                    _newValue = value;
                    OnPropertyChanged(nameof(NewValue));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _newValue;
        private readonly ButtonSetting _buttonSetting;

        public ButtonValueChangingViewModel(ButtonSetting buttonSetting) =>
            (_buttonSetting, _newValue) = (buttonSetting, buttonSetting.SettedValue.ToString());

        public void AcceptСhanges()
        {
            _buttonSetting.SettedValue = String.IsNullOrEmpty(_newValue) ? (char?)null : _newValue[0];
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
