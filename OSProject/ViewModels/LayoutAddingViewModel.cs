using Newtonsoft.Json;
using OSProject.Models;
using OSProject.Models.Config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OSProject.ViewModels
{
    public class LayoutAddingViewModel : INotifyPropertyChanged
    {
        // !TODO добавить возможность сделать все значения кнопок null-м
        // !TODO При надобности создать модели для соблюдения уникального нейминга раскладок (такое в теории может понадобится где-то ещё)

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
        public event PropertyChangedEventHandler PropertyChanged;

        private string _newLayoutName;
        private readonly DefaultLayoutConfig _layoutConfig;
        private readonly string _layoutsDirectoryRoot;

        public LayoutAddingViewModel(DefaultLayoutConfig layoutConfig, string layoutsDirectoryRoot)
        {
            if (layoutConfig is null)
                throw new ArgumentNullException(nameof(layoutConfig));

            _layoutConfig = layoutConfig;
            NewLayoutName = "Название";
            ButtonsSetting = new ObservableCollection<ButtonSetting>();
            foreach (var line in _layoutConfig.GetDefaultLayout())
                foreach (KeyboardButton button in line)
                    ButtonsSetting.Add(new ButtonSetting(button, _layoutConfig.GetCharacterById(button.Id)));

            _layoutsDirectoryRoot = layoutsDirectoryRoot;
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

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
