using Newtonsoft.Json;
using OSProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace OSProject.ViewModels
{
    public class LayoutAddingViewModel : INotifyPropertyChanged
    {
        // !TODO добавить возможность сделать все значения кнопок null-м
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
        private string _layoutsDirectoryRoot = @"C:\Users\vitia\source\repos\C#\WPF\OSProject\OSProject\Layouts\";

        public LayoutAddingViewModel(DefaultKeyboardLayoutConfig layoutConfig)
        {
            if (layoutConfig is null)
                throw new ArgumentNullException(nameof(layoutConfig));

            _layoutConfig = layoutConfig;
            NewLayoutName = "Название";
            ButtonsSetting = new ObservableCollection<ButtonSetting>();
            foreach (var line in _layoutConfig.GetDefaultLayout())
                foreach (KeyboardButton button in line)
                    ButtonsSetting.Add(new ButtonSetting(button, _layoutConfig.GetCharacterById(button.Id)));

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

        public void CreateNewLayout()
        {
            if (String.IsNullOrEmpty(_newLayoutName))
            {
                throw new InvalidOperationException("Имя раскаладки должо быть не пустым");
            }
            if (!ButtonsSetting.Any(setting => !(setting.SettedValue is null)))
            {
                throw new InvalidOperationException("Хотя бы одна кнопка должна быть не пустой");
            }

            var builder = new List<List<KeyboardButton>>();

            int buttonId = 0;
            foreach (string str in _layoutConfig.Lines)
            {
                var line = new List<KeyboardButton>();
                for (int i = 0; i < str.Length; i++)
                {
                    if (!(ButtonsSetting[buttonId].SettedValue is null))
                    {
                        line.Add(new KeyboardButton(buttonId, ButtonsSetting[buttonId].SettedValue));
                    }
                    buttonId++;
                }
                builder.Add(line);
            }
            KeyboardLayout layout = new KeyboardLayout(_newLayoutName, builder);

            File.WriteAllText(
                Path.Combine(_layoutsDirectoryRoot, _newLayoutName + ".json"),
                JsonConvert.SerializeObject(layout, Formatting.Indented)
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
