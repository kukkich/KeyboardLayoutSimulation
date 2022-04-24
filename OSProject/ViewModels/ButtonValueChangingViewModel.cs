using OSProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.ViewModels
{
    public class ButtonValueChangingViewModel : INotifyPropertyChanged
    {
        private ButtonSetting _buttonSetting { get; set; }

        public string NewValue { 
            get => _newValue;
            set {
                if (value.Length < 2)
                {
                    _newValue = value;
                    OnPropertyChanged(nameof(NewValue));
                }
            }
        }

        private string _newValue;

        public ButtonValueChangingViewModel(ButtonSetting buttonSetting) =>
            (_buttonSetting, _newValue) = (buttonSetting, buttonSetting.SettedValue.ToString());

        public void AcceptСhanges()
        {
            _buttonSetting.SettedValue = String.IsNullOrEmpty(_newValue) ? (char?)null : _newValue[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
