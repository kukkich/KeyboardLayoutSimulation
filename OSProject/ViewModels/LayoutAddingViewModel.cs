using OSProject.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<KeyboardButton> Buttons { get; set; }

        private string _newLayoutName;
        private DefaultKeyboardLayoutConfig _layoutConfig;

        public LayoutAddingViewModel(DefaultKeyboardLayoutConfig layoutConfig)
        {
            _layoutConfig = layoutConfig;
            NewLayoutName = "Название";

            Buttons = new ObservableCollection<KeyboardButton>();
            foreach (var line in _layoutConfig.GetDefaultLayout())
            {
                foreach (KeyboardButton button in line)
                {
                    Buttons.Add(button);
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
