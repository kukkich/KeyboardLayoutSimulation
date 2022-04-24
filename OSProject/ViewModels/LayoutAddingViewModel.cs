using OSProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OSProject.ViewModels
{
    public class LayoutAddingViewModel : INotifyPropertyChanged
    {
        public DefaultKeyboardLayoutConfig LayoutConfig { get => _layoutConfig; }
        public string NewLayoutName
        {
            get => _newLayoutName;
            set
            {
                _newLayoutName = value;
                OnPropertyChanged(nameof(NewLayoutName));
            }
        }
        public ObservableCollection<ButtonSetting> ButtonsSetting { get; set; }

        private string _newLayoutName;
        private DefaultKeyboardLayoutConfig _layoutConfig;

        public LayoutAddingViewModel(DefaultKeyboardLayoutConfig layoutConfig)
        {
            _layoutConfig = layoutConfig;
            NewLayoutName = "Название";
            ButtonsSetting = new ObservableCollection<ButtonSetting>();
            foreach (var line in _layoutConfig.GetDefaultLayout())
            {
                foreach (KeyboardButton button in line)
                {
                    ButtonsSetting.Add(new ButtonSetting(button, _layoutConfig.GetCharacterById(button.Id)));
                    //button.Value = null;
                }
            }

        }

        public List<KeyboardButton> GetButtons()
        {
            List<KeyboardButton> buttons = new List<KeyboardButton>();
            foreach (var line in _layoutConfig.GetDefaultLayout())
            {
                foreach (KeyboardButton button in line)
                {
                    buttons.Add(button);
                }
            }
            return buttons;
        }

        public void ChangeButtonValue(int buttonId, char? newValue)
        {
            var searchedButton = ButtonsSetting.First(buttonSetting => buttonSetting.Button.Id == buttonId).Button;
            if (searchedButton is null) throw new ArgumentOutOfRangeException(nameof(buttonId));
            searchedButton.Value = newValue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
